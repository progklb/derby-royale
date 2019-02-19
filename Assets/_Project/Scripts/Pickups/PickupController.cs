using UnityEngine;

using Random = UnityEngine.Random;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Controller")]
    public sealed class PickupController : MonoBehaviour
    {
        #region CONSTANTS
        private const string PICKUPS_RESOURCE_FOLDER = "Resources/Pickups";
        #endregion


        #region PROPERTIES
        private bool spawnOnStart { get => m_SpawnOnStart; set => m_SpawnOnStart = value; }
        private bool spawnBeforeDelay { get => m_SpawnBeforeDelay; set => m_SpawnBeforeDelay = value; }
        private float minimumSpawnDelay { get => m_MinimumSpawnDelay; set => m_MinimumSpawnDelay = value; }
        private float maximumSpawnDelay { get => m_MaximumSpawnDelay; set => m_MaximumSpawnDelay = value; }
        private PickupSpawner[] pickupSpawners { get => m_PickupSpawners; set => m_PickupSpawners = value; }

        public GameObject[] pickupLibrary { get => m_PickupLibrary ?? (m_PickupLibrary = Resources.LoadAll(PICKUPS_RESOURCE_FOLDER) as GameObject[]); }
        #endregion


        #region EDITOR FIELDS
        [Header("PICKUP SPAWN SETUP"), Space(3)]
        [SerializeField]
        private bool m_SpawnOnStart = true;
        [SerializeField]
        private bool m_SpawnBeforeDelay = true;
        [SerializeField, Range(1f, 120f)]
        private float m_MinimumSpawnDelay = 15f;
        [SerializeField, Range(1f, 120f)]
        private float m_MaximumSpawnDelay = 30f;
        [SerializeField]
        private PickupSpawner[] m_PickupSpawners;

        private GameObject[] m_PickupLibrary;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            InitializePickupSpawners();
        }
        #endregion


        #region PUBLIC API
        public void TogglePickupSpawners(bool running)
        {
            if (pickupSpawners != null)
            {
                foreach(PickupSpawner spawner in pickupSpawners)
                {
                    spawner.ToggleSpawner(running);
                }
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        void InitializePickupSpawners()
        {
            if (pickupSpawners != null)
            {
                foreach(PickupSpawner spawner in pickupSpawners)
                {
                    spawner.InitializeSpawner(this, Random.Range(minimumSpawnDelay, maximumSpawnDelay), spawnBeforeDelay);
                    spawner.ToggleSpawner(spawnOnStart);
                }
            }
        }
        #endregion
    }
}