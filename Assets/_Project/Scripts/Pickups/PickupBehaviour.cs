using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    public abstract class PickupBehaviour
    {
        #region PROPERTIES
        public string pickupName { get => m_PickupName; set => m_PickupName = value; }
        public Sprite pickupIcon { get => m_PickupIcon; set => m_PickupIcon = value; }
        #endregion


        #region EDITOR FIELDS
        [Space(3), Header("PICKUP SETUP"), Space(5)]
        [SerializeField]
        private string m_PickupName;
        [SerializeField]
        private Sprite m_PickupIcon;
        #endregion


        #region PUBLIC API
        public virtual void UsePickup(Vehicle owningDerbyCar)
        {

        }
        #endregion
    }
}