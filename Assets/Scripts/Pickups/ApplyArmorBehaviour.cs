using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Behaviours/Apply Armor Behaviour")]
    public class ApplyArmorBehaviour : PickupBehaviour
    {
        #region PROPERTIES
        public float armoredDuration { get => m_ArmoredDuration; }
        #endregion


        #region EDITOR FIELDS
        [Space(10), Header("ARMOR SETTINGS"), Space(5)]
        [SerializeField, Range(0.1f, 20f), Tooltip("Adds armor against damage to the vehicle for 'Armored Duration' in seconds.")]
        private float m_ArmoredDuration = 10f;
        #endregion


        #region PUBLIC API
        public override void UsePickup(DerbyCar owningDerbyCar)
        {
            owningDerbyCar.ApplyCarArmor(armoredDuration);
        }
        #endregion
    }
}
