using UnityEngine;

namespace DerbyRoyal.Vehicles
{
    [AddComponentMenu("Derby Royal/Vehicles/Vehicle Controller")]
    public class VehicleController : MonoBehaviour
    {
        #region PROPERTIES
        private const string ACCELERATE_AXIS = "Vertical";
        private const string TURN_AXIS = "Horizontal";
        #endregion


        #region VARIABLES
        public float acceleration { get => Input.GetAxis(ACCELERATE_AXIS); }
        public float turning { get => Input.GetAxis(TURN_AXIS); }
        public bool isAccelerating { get => acceleration != 0f; }
        public bool isTurning { get => turning != 0f; }
        #endregion
    }
}