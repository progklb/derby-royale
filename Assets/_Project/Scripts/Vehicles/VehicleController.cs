using UnityEngine;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Vehicles
{
    [AddComponentMenu("Derby Royale/Vehicles/Vehicle Controller")]
    public class VehicleController : MonoBehaviour
    {
        #region PROPERTIES
        private const string ACCELERATE_AXIS = "Vertical_K1";
        private const string TURN_AXIS = "Horizontal_K1";
        #endregion


        #region VARIABLES
        public float acceleration { get => UInput.GetAxis(ACCELERATE_AXIS); }
        public float turning { get => UInput.GetAxis(TURN_AXIS); }

        public bool isAccelerating { get => acceleration != 0f; }
        public bool isTurning { get => turning != 0f; }
        #endregion
    }
}