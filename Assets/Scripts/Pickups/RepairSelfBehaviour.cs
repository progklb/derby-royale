using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Behaviours/Repair Self Behaviour")]
    public class RepairSelfBehaviour : PickupBehaviour
    {
        #region PROPERTIES
        public float repairAmount { get => m_RepairAmount * 0.01f; }
        #endregion


        #region EDITOR FIELDS
        [Space(10), Header("REPAIR SETTINGS"), Space(5)]
        [SerializeField, Range(1, 100), Tooltip("Repairs the owning vehicle in the range of 1% to 100%.")]
        private int m_RepairAmount = 30;
        #endregion


        #region PUBLIC API
        public override void UsePickup(DerbyCar owningDerbyCar)
        {
            owningDerbyCar.RepairCar(repairAmount);
        }
        #endregion
    }
}