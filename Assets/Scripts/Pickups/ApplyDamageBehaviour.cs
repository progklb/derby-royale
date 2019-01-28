using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Behaviours/Apply Damage Behaviour")]
    public class ApplyDamageBehaviour : PickupBehaviour
    {
        #region PROPERTIES
        public float damageAmount { get => m_DamageAmount * 0.01f; }
        #endregion


        #region EDITOR FIELDS
        [Space(10), Header("DAMAGE SETTINGS"), Space(5)]
        [SerializeField, Range(1, 100), Tooltip("Applies damage in the range of 1% to 100%.")]
        private int m_DamageAmount;
        #endregion


        #region PUBLIC API
        public override void UsePickup(DerbyCar owningDerbyCar)
        {
            owningDerbyCar.DamageCar(damageAmount);
        }
        #endregion
    }
}