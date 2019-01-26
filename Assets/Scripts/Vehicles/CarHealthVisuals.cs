using UnityEngine;

namespace DerbyRoyale.Vehicles
{
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class CarHealthVisuals : MonoBehaviour
    {
        #region CONSTANTS
        private const int MAXIMUM_SMOKE_EMISSION_RATE = 50;
        private const float MINIMUM_SMOKE_SCALE = 0.1f;
        private const float MAXIMUM_SMOKE_SCALE = 1f;
        #endregion


        #region PROPERTIES
        private DerbyCar owningDerbyCar { get => m_OwningDerbyCar; }
        private ParticleSystem engineSmokeParticles { get => m_EngineSmokeParticles ?? (m_EngineSmokeParticles = GetComponent<ParticleSystem>()); }
        #endregion


        #region EDITOR FIELDS
        [Space(3), Header("HEALTH VISUALIZER SETUP"), Space(5)]
        [SerializeField]
        private DerbyCar m_OwningDerbyCar;
        #endregion


        #region VARIABLES
        private ParticleSystem m_EngineSmokeParticles;
        #endregion


        #region UNITY EVENTS
        void Update()
        {
            if (!owningDerbyCar.hasMaxHealth)
            {
                EmitSmokeParticles();
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        void EmitSmokeParticles()
        {
            var smokeParams = new ParticleSystem.EmitParams();
            smokeParams.startSize = 0.2f;

            int emissionRate = Mathf.FloorToInt(Mathf.Lerp(0, MAXIMUM_SMOKE_EMISSION_RATE, owningDerbyCar.currentHealth));
            engineSmokeParticles.Emit(smokeParams, emissionRate);
        }
        #endregion
    }
}