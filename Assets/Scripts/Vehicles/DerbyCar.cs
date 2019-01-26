using UnityEngine;

namespace DerbyRoyale.Vehicles
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(MeshFilter))]
    [RequireComponent(typeof(VehicleController))]
    [AddComponentMenu("Derby Royale/Vehicles/Derby Car")]
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
        private float DEFAULT_ACCELERATION = 1000f;
        private float TURN_RATE = 5.0f;
        #endregion


        #region PROPERTIES
        private Rigidbody rigidBody { get => m_RigidBody ?? (m_RigidBody = GetComponent<Rigidbody>()); }
        private Vector3 forwardAcceleration { get => transform.forward * DEFAULT_ACCELERATION * Time.deltaTime; }
        private Vector3 rightTurningTorque { get => transform.up * (vehicleController.turning * TURN_RATE * Time.deltaTime); }

        private VehicleController vehicleController { get => m_VehicleController ?? (m_VehicleController = GetComponent<VehicleController>()); }
        #endregion


        #region VARIABLES
        private Rigidbody m_RigidBody;
        private VehicleController m_VehicleController;
        #endregion


        #region UNITY EVENTS
        void FixedUpdate()
        {
            CarEngine();
        }
        #endregion


        #region PUBLIC API
        public void AccelerateCar()
        {
            rigidBody.AddForce(forwardAcceleration, ForceMode.Acceleration);
        }

        public void TurnCar()
        {
            rigidBody.AddTorque(rightTurningTorque, ForceMode.Force);
        }
        #endregion


        #region HELPER FUNCTIONS
        void CarEngine()
        {
            if (vehicleController.isAccelerating)
            {
                AccelerateCar();
            }

            if (vehicleController.isTurning)
            {
                TurnCar();
            }
        }
        #endregion
    }
}