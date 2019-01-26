using UnityEngine;

namespace DerbyRoyale.Vehicles
{
    public sealed class FloorDetectionComponent : MonoBehaviour
    {
        #region CONSTANTS
        private const float DETECTION_DISTANCE = 50f;
        #endregion


        #region PROPERTIES
        public bool isGrounded { get => Physics.Raycast(transform.position, -transform.up, DETECTION_DISTANCE); }
        #endregion
    }
}