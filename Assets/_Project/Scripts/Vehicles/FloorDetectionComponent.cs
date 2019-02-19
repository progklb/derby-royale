using UnityEngine;

namespace DerbyRoyale.Vehicles
{
    public sealed class FloorDetectionComponent : MonoBehaviour
    {
        #region CONSTANTS
        private const float DETECTION_DISTANCE = 0.25f;
        #endregion


        #region PROPERTIES
        private bool drawDetectionDebugLine { get => m_DrawDetectionDebugLine; }
        public bool isGrounded { get => Physics.Raycast(transform.position, -transform.up, DETECTION_DISTANCE, LayerMask.GetMask(Layers.FLOOR_LAYER)); }
        #endregion


        #region EDITOR FIELDS
        [SerializeField]
        private bool m_DrawDetectionDebugLine;
        #endregion


        #region UNITY EVENTS
        void FixedUpdate()
        {
            if (drawDetectionDebugLine)
            {
                DrawDebugLine();
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        void DrawDebugLine()
        {
            Debug.DrawLine(transform.position, transform.position + (-transform.up * DETECTION_DISTANCE), Color.green);
        }
        #endregion
    }
}