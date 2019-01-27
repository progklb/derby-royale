using UnityEngine;

namespace DerbyRoyale.Pickups
{
    [RequireComponent(typeof(PickupBehaviour))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider), typeof(MeshFilter), typeof (MeshRenderer))]
    [AddComponentMenu("Derby Royale/Pickups/Derby Pickup")]
    public sealed class DerbyPickup : MonoBehaviour
    {
        #region CONSTANTS
        private const string SPAWNED_TRIGGER = "Spawned";
        private const string PICKED_UP_TRIGGER = "PickedUp";
        #endregion


        #region PROPERTIES
        private bool hasSpawned { get; set; }
        private PickupBehaviour pickupBehaviour { get => m_PickupBehaviour ?? (m_PickupBehaviour = GetComponent<PickupBehaviour>()); set => m_PickupBehaviour = value; }
        private Animator pickupAnimator { get => m_PickupAnimator ?? (m_PickupAnimator = GetComponent<Animator>()); set => m_PickupAnimator = value; }
        #endregion


        #region VARIABLES
        private PickupBehaviour m_PickupBehaviour;
        private Animator m_PickupAnimator;
        #endregion


        #region UNITY EVENTS
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == Tags.PLAYER_TAG)
            {
                if (hasSpawned)
                {

                }
            }
        }
        #endregion
    }
}