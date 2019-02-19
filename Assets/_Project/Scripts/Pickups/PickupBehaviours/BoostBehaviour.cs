using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups.PickupBehaviours
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Behaviours/Boost Behaviour")]
    public class BoostBehaviour : PickupBehaviour
    {
        #region PROPERTIES
        public float boostDuration { get => m_BoostDuration; }
        #endregion


        #region EDITOR FIELDS
        [Space(10), Header("BOOST SETTINGS"), Space(5)]
        [SerializeField, Range(0.1f, 20f), Tooltip("Increases the vehicle's speed for 'Boost Duration' in seconds.")]
        private float m_BoostDuration = 3f;
        #endregion


        #region PUBLIC API
        public override void UsePickup(Vehicle owningDerbyCar)
        {
            owningDerbyCar.BoostCar(boostDuration);
        }
        #endregion
    }
}