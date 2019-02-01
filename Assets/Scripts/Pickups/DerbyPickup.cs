using UnityEngine;

using System.Collections;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider), typeof(MeshFilter), typeof (MeshRenderer))]
    [AddComponentMenu("Derby Royale/Pickups/Derby Pickup")]
    public sealed class DerbyPickup : MonoBehaviour
    {
        #region CONSTANTS
        private const string LANDED_TRIGGER = "Landed";
        private const string PICKED_UP_TRIGGER = "PickedUp";
        private const string PICKUP_CONSUMED_BOOL = "PickupConsumed";
        #endregion


        #region PROPERTIES
        private PickupBehaviour[] pickupEffects { get => m_PickupEffects ?? (m_PickupEffects = GetComponents<PickupBehaviour>()); set => m_PickupEffects = value; }
        private Animator pickupAnimator { get => m_PickupAnimator ?? (m_PickupAnimator = GetComponent<Animator>()); set => m_PickupAnimator = value; }
        private bool pickupActive { get; set; }
        private bool pickupConsumed { get => pickupAnimator.GetBool(PICKUP_CONSUMED_BOOL); }
        #endregion


        #region VARIABLES
        private PickupBehaviour[] m_PickupEffects;
        private Animator m_PickupAnimator;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            pickupActive = true;
        }

        void OnCollisionEnter(Collision col)
        {
            if (pickupActive)
            {
                ConsumePickup(col);
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        void ConsumePickup(Collision collision)
        {
            if (collision.gameObject.tag == Tags.PLAYER_TAG)
            {
                var derbyCar = collision.gameObject.GetComponent<DerbyCar>();

                if (pickupEffects != null)
                {
                    derbyCar.AddCarPickup(pickupEffects);
                }
            }
        }

        IEnumerator DestroyPickup()
        {
            pickupActive = false;
            pickupAnimator.SetTrigger(PICKED_UP_TRIGGER);
            yield return new WaitUntil(() => pickupConsumed);
            Destroy(gameObject);
        }
        #endregion
    }
}