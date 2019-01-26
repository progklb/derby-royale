using UnityEngine;

namespace DerbyRoyal.Vehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshCollider), typeof(MeshRenderer))]
    [RequireComponent(typeof(VehicleController))]
    [AddComponentMenu("Derby Royal/Vehicles/Derby Car")]
    public class DerbyCar : MonoBehaviour
    {
        #region NESTED TYPES
        private enum CarAcceleration
        {
            ByTorque,
            ByForce
        }

        private enum CarSteering
        {
            ByRotation,
            ByAngularDampening,
        }
        #endregion


        #region CONSTANTS
        private float DEFAULT_ACCELERATION = 100f;
        private float TURN_RATE = 0.5f;
        #endregion


        #region PROPERTIES
        private Rigidbody rigidBody { get => m_RigidBody ?? (m_RigidBody = GetComponent<Rigidbody>()); }
        private Vector3 forwardAcceleration { get => Vector3.forward * DEFAULT_ACCELERATION * Time.deltaTime; }
        private CarAcceleration carAcceleration { get => m_CarAcceleration; }
        private Vector3 rightTurningRotation { get => Vector3.right * TURN_RATE * Time.deltaTime; }
        private Vector3 rightTurningTorque { get => transform.up * ((vehicleController.turning * TURN_RATE * Time.deltaTime) * 10000f); }
        private CarSteering carSteering { get => m_CarSteering; }

        private VehicleController vehicleController { get => m_VehicleController ?? (m_VehicleController = GetComponent<VehicleController>()); }
        #endregion


        #region EDITOR FIELDS
        [Space(3), Header("DERBY CAR SETUP"), Space(5)]
        [SerializeField]
        private CarAcceleration m_CarAcceleration = CarAcceleration.ByForce;
        [SerializeField]
        private CarSteering m_CarSteering = CarSteering.ByRotation;
        #endregion


        #region VARIABLES
        private Rigidbody m_RigidBody;
        private VehicleController m_VehicleController;
        #endregion


        #region PUBLIC API
        public void AccelerateCar()
        {
            if (carAcceleration == CarAcceleration.ByForce)
            {
                rigidBody.AddForce(forwardAcceleration, ForceMode.Acceleration);
            }
            else
            {
                rigidBody.AddTorque(forwardAcceleration, ForceMode.Acceleration);
            }
        }

        public void TurnCar()
        {
            if (carSteering == CarSteering.ByRotation)
            {
                transform.Rotate(rightTurningRotation);
            }
            else
            {
                rigidBody.AddRelativeTorque(rightTurningTorque);
            }
        }
        #endregion
    }
}