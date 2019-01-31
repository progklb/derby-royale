using UnityEngine;

using System.Collections;

using Random = UnityEngine.Random;

namespace DerbyRoyale.Pickups
{
    [AddComponentMenu("Derby Royale/Pickups/Pickup Spawner")]
    public class PickupSpawner : MonoBehaviour
    {
        #region PROPERTIES
        public bool overridePickupRandomization { get => m_OverridePickupRandomization && pickupOverride != null; }
        public bool spawnBeforeDelay { get; set; }
        public bool isRunning { get; private set; }
        public float spawnDelaySeconds { get; private set; }

        public bool isInitialized { get => pickupController != null; }
        private PickupController pickupController { get; set; }
        private GameObject pickupOverride { get => m_PickupOverride; set => m_PickupOverride = value; }
        #endregion


        #region EDITOR FIELDS
        [Header("PICKUP SETUP"), Space(3)]
        [SerializeField]
        private bool m_OverridePickupRandomization;
        [SerializeField]
        private GameObject m_PickupOverride;
        #endregion


        #region PUBLIC API
        /// <summary>
        /// Pickup Spawners require a Pickup Controller to run, and need to be initialized by the controller on start.
        /// </summary>
        public void InitializeSpawner(PickupController owningPickupController, float spawnDelayInSeconds, bool spawnBeforeDelay = true)
        {
            spawnDelaySeconds = spawnDelayInSeconds;
            pickupController = owningPickupController;
        }

        public void ToggleSpawner(bool running)
        {
            if (isInitialized)
            {
                isRunning = running;

                if (isRunning)
                {
                    StartCoroutine(PickupSpawnDelay());
                }
            }
            else
            {
                isRunning = false;
            }
        }

        public void SpawnPickup()
        {
            if (overridePickupRandomization)
            {
                SpawnSpecificPickup();
            }
            else
            {
                SpawnRandomPickup();
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        IEnumerator PickupSpawnDelay()
        {
            do
            {
                if (spawnBeforeDelay)
                {
                    SpawnPickup();
                }
                yield return new WaitForSeconds(spawnDelaySeconds);
                if (!spawnBeforeDelay)
                {
                    SpawnPickup();
                }
            } while (isRunning);
        }

        void SpawnRandomPickup()
        {
            Instantiate(pickupController.pickupLibrary[Random.Range(0, pickupController.pickupLibrary.Length - 1)], transform.position, new Quaternion());
        }

        void SpawnSpecificPickup()
        {
            Instantiate(pickupOverride, transform.position, new Quaternion());
        }
        #endregion
    }
}