using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Behaviours/Slide Behaviour")]
    public class SlideBehaviour : PickupBehaviour
    {
        #region PROPERTIES
        public float slidingDuration { get => m_SlidingDuration; }
        #endregion


        #region EDITOR FIELDS
        [Space(10), Header("SLIDE SETTINGS"), Space(5)]
        [SerializeField, Range(0.1f, 20f), Tooltip("Makes the vehicle's handling feel 'slippery' for 'Sliding Duration' in seconds.")]
        private float m_SlidingDuration = 3f;
        #endregion


        #region PUBLIC API
        public override void UsePickup(DerbyCar owningDerbyCar)
        {
            owningDerbyCar.SlipCar(slidingDuration);
        }
        #endregion
    }
}