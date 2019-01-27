using UnityEngine;

using System;
using System.Collections;

using DerbyRoyale.Pickups;

using URandom = UnityEngine.Random;

namespace DerbyRoyale.Vehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(MeshFilter))]
    [RequireComponent(typeof(VehicleController))]
    [AddComponentMenu("Derby Royale/Vehicles/Derby Car")]
    public sealed class DerbyCar : MonoBehaviour
    {
        #region CONSTANTS
        private const float DEFAULT_ACCELERATION = 1750f;
        private const float DEFAULT_DRAG = 1.75f;
        private const float DEFAULT_ANGULAR_DRAG = 0.2f;
        private const float SLIPPERY_DRAG = 0.8f;
        private const float SLIPPERY_ANGULAR_DRAG = 0.05f;
        private const float TURN_RATE = 25f;
        private const float MAXIMUM_VELOCITY = 100f;
        private const float FLIP_IMPULSE_STRENGTH = 10f;
        private const float FLIP_RESET_DURATION = 2f;

        private const float MINIMUM_CRASH_VELOCITY = 11f;
        private const float MAXIMUM_CRASH_VELOCITY = 24f;
        private const float MAXIMUM_CRASH_DAMAGE = 0.5f;

        private const float DEFAULT_HEALTH = 1f;
        private const float BOOST_MULTIPLIER = 1.5f;
        private const float ARMOR_MULTIPLIER = 0.5f;
        private const float TEMPORARY_SLIP_DURATION = 3f;

        private const float TRASHED_DESTRUCTION_DELAY = 3f;
        private const float TRASHED_EXPLOSION_FORCE = 10f;
        private const float TRASHED_EXPLOSION_RADIUS = 5f;
		#endregion


		#region EVENTS
		public static event Action<DerbyCar> onSpawn = delegate { };
		public static event Action<DerbyCar> onDeath = delegate { };
		#endregion


		#region PROPERTIES
		private Rigidbody rigidBody { get => m_RigidBody ?? (m_RigidBody = GetComponent<Rigidbody>()); }
        private Vector3 forwardAcceleration { get => ((transform.forward * DEFAULT_ACCELERATION) * vehicleController.acceleration) * Time.deltaTime; }
        private Vector3 rightTurning { get => (((transform.up * vehicleController.turning) * TURN_RATE) * Mathf.Lerp(0f, TURN_RATE, rigidBody.velocity.magnitude / MAXIMUM_VELOCITY))* Time.deltaTime; }

        private VehicleController vehicleController { get => m_VehicleController ?? (m_VehicleController = GetComponent<VehicleController>()); }
        private FloorDetectionComponent[] floorDetectionComponents { get => m_FloorDetectionComponents; }
        private bool reverseInputInverted { get => m_InvertReverseInput; }

        public float currentHealth { get => Mathf.Clamp01(m_CurrentHealth); set { m_CurrentHealth = Mathf.Clamp01(value); } }
        public bool hasMaxHealth { get => currentHealth == 1f; }
        public bool isTrashed { get => currentHealth == 0f; }
        public bool isBoosting { get; private set; }
        public bool isArmored { get; private set; }
        public bool isGrounded { get => RefreshFloorDetection(); }
        public bool isSlipping { get; private set; }
        private bool isReversing { get => vehicleController.acceleration < 0f; }
        private bool canFlip { get; set; }
        private bool isFlipping { get => Input.GetKeyDown(KeyCode.Space); }
        private bool hasPickup { get => currentPickup != null; }

        private PickupBehaviour currentPickup { get; set; }
        #endregion


        #region EDITOR FIELDS
        [Space(3), Header("DERBY CAR SETUP"), Space(5)]
        [SerializeField]
        private bool m_InvertReverseInput;
        [Space(5)]
        [SerializeField]
        private FloorDetectionComponent[] m_FloorDetectionComponents;
        #endregion


        #region VARIABLES
        private Rigidbody m_RigidBody;
        private VehicleController m_VehicleController;
        private float m_CurrentHealth;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            RestartCar();

			onSpawn(this);
        }

        void FixedUpdate()
        {
            CarEngine();
        }

        void OnCollisionEnter(Collision col)
        {
            CrashCar(col);
        }
        #endregion


        #region PUBLIC API
        public void RestartCar()
        {
            currentHealth = DEFAULT_HEALTH;
            rigidBody.drag = DEFAULT_DRAG;
            rigidBody.angularDrag = DEFAULT_ANGULAR_DRAG;
            isBoosting = false;
            isArmored = false;
            isSlipping = false;
            canFlip = true;
        }

        public void DamageCar(float damageAmount)
        {
            if (isArmored)
            {
                currentHealth -= damageAmount * ARMOR_MULTIPLIER;
            }
            else
            {
                currentHealth -= damageAmount;
            }

            if (isTrashed)
            {
                TrashCar();
            }
        }

        public void RepairCar(float repairAmount)
        {
            if (isTrashed)
            {
                return;
            }

            currentHealth += repairAmount;
        }

        public void BoostCar(float boostDuration)
        {
            if (isTrashed)
            {
                return;
            }

            if (!isBoosting)
            {
				StartCoroutine(RunBoostTimer(boostDuration));
            }
        }

        public void ApplyCarArmor(float armorDuration)
        {
            if (isTrashed)
            {
                return;
            }

            if (!isArmored)
            {
				StartCoroutine(RunArmorTimer(armorDuration));
            }
        }

        public bool AddCarPickup(PickupBehaviour newPickup, bool overrideCurrentPickup = false)
        {
            if (isTrashed)
            {
                return false;
            }

            if (!hasPickup || overrideCurrentPickup)
            {
                currentPickup = newPickup;
                return true;
            }
            return false;
        }

        public void UseCarPickup()
        {
            if (hasPickup && !isTrashed)
            {
                currentPickup.UsePickup(this);
                currentPickup = null;
            }
        }

        public void SlipCar(float slippingDuration = TEMPORARY_SLIP_DURATION)
        {
            if (!isSlipping)
            {
				StartCoroutine(RunSlippingTimer(slippingDuration));
            }
        }

        public void TrashCar()
        {
			if (!isTrashed)
			{
				currentHealth = 0f;
				isBoosting = false;
				isArmored = false;

				rigidBody.AddExplosionForce(TRASHED_EXPLOSION_FORCE, transform.position, TRASHED_EXPLOSION_RADIUS, 1.5f, ForceMode.Impulse);
				StartCoroutine(RunDestructionTimer());

				onDeath(this);
			}
        }
        #endregion


        #region HELPER FUNCTIONS
        void CarEngine()
        {
            if (isTrashed)
            {
                return;
            }

            if (isFlipping && canFlip)
            {
                FlipCar();
            }

            if (vehicleController.isAccelerating && isGrounded)
            {
                if (isReversing)
                {
                    ReverseCar();
                }
                else
                {
                    AccelerateCar();
                }
            }

            if (vehicleController.isTurning && isGrounded)
            {
                TurnCar();
            }
        }

        void AccelerateCar()
        {
            if (isBoosting)
            {
                rigidBody.AddForce(forwardAcceleration * BOOST_MULTIPLIER, ForceMode.Acceleration);
            }
            else
            {
                rigidBody.AddForce(forwardAcceleration, ForceMode.Acceleration);
            }
        }

        void ReverseCar()
        {
            if (isBoosting)
            {
                rigidBody.AddForce(Vector3.Reflect(forwardAcceleration, transform.up) * 0.75f, ForceMode.Acceleration);
            }
            else
            {
                rigidBody.AddForce(Vector3.Reflect(forwardAcceleration, transform.up) * 0.5f, ForceMode.Force);
            }
        }

        void TurnCar()
        {
            Quaternion deltaRotation = Quaternion.Euler(rightTurning);

            if (reverseInputInverted && isReversing)
            {
                deltaRotation = Quaternion.Euler(-rightTurning);
            }

            rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        }

        bool RefreshFloorDetection()
        {
            int count = 0;

            foreach(FloorDetectionComponent detection in floorDetectionComponents)
            {
                if (detection.isGrounded)
                {
                    ++count;

                    if (count >= floorDetectionComponents.Length / 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void CrashCar(Collision collision)
        {
            if (collision.relativeVelocity.magnitude < MINIMUM_CRASH_VELOCITY)
            {
                return;
            }

            float crashDamage = Mathf.Clamp01(collision.relativeVelocity.magnitude / MAXIMUM_CRASH_VELOCITY);
            float damageMultiplier = 1f;

            if(isBoosting)
            {
                damageMultiplier = BOOST_MULTIPLIER;
            }

            DamageCar(Mathf.Lerp(0f, MAXIMUM_CRASH_DAMAGE / damageMultiplier, crashDamage));
        }

        void FlipCar()
        {
            if (isGrounded || !canFlip)
            {
                return;
            }

            Vector3 randomOffset = -transform.up + new Vector3(URandom.Range(-0.1f,0.1f), URandom.Range(-0.1f, 0.1f), URandom.Range(-0.1f, 0.1f));
            rigidBody.AddForce(randomOffset * FLIP_IMPULSE_STRENGTH, ForceMode.Impulse);
        }

        IEnumerator RunBoostTimer(float boostDuration)
        {
            isBoosting = true;
            yield return new WaitForSeconds(boostDuration);
            isBoosting = false;
        }

        IEnumerator RunArmorTimer(float armorDuration)
        {
            isArmored = true;
            yield return new WaitForSeconds(armorDuration);
            isArmored = false;
        }

        IEnumerator RunDestructionTimer()
        {
			Debug.Log("Running destruction timer");
            yield return new WaitForSeconds(TRASHED_DESTRUCTION_DELAY);
            Destroy(gameObject);
			Debug.Log("Vehicle destroyed");
		}

		IEnumerator RunSlippingTimer(float slippingDuration)
        {
            isSlipping = true;
            rigidBody.drag = SLIPPERY_DRAG;
            rigidBody.angularDrag = SLIPPERY_ANGULAR_DRAG;
            yield return new WaitForSeconds(slippingDuration);
            rigidBody.drag = DEFAULT_DRAG;
            rigidBody.angularDrag = DEFAULT_ANGULAR_DRAG;
            isSlipping = false;
        }

        IEnumerator RunFlipResetTimer()
        {
            canFlip = false;
            yield return new WaitForSeconds(FLIP_RESET_DURATION);
            canFlip = true;
        }
        #endregion
    }
}