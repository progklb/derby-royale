using UnityEngine;

using System;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Represents a single stage (tier) of a level.
	/// </summary>
	public class Stage : MonoBehaviour
	{
		#region EVENTS
		public static event Action<Stage, StageProgress> onStageProgressChanged = delegate { };
		#endregion


		#region TYPES
		[Serializable]
		public class StageParameters
		{
			public float stageTimeout { get => m_StageTimeout; }
			public float stageCompleteTimeout { get => m_StageCompleteTimeout; }
			public float stageDestroyTimeout { get => m_StageDestroyTimeout; }

			/// Amount of time that the stage will last for.
			[SerializeField] private float m_StageTimeout;
			/// Amount of time to wait for the stage complete sequence to finish (approach the next tier)
			[SerializeField] private float m_StageCompleteTimeout;
			/// Amount of time after the stage ends until the stage self-destructs.
			[SerializeField] private float m_StageDestroyTimeout;
		}
		#endregion


		#region PROPERTIES
		/// Defines the parameters this stage.
		public StageParameters parameters { get => m_StageParameters; }

		public int stageNumber { get; set; }

		private StageBehaviour stageBehaviour { get; set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private StageParameters m_StageParameters;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			stageBehaviour = GetComponent<StageBehaviour>();

			if (stageBehaviour != null)
			{
				stageBehaviour.onStageProgressChanged += HandleStageProgressChanged;
			}
			else
			{
				Debug.LogError($"A {nameof(StageBehaviour)} must be assigned to this instance.");
			}
		}

		void OnDisabel()
		{
			if (stageBehaviour != null)
			{
				stageBehaviour.onStageProgressChanged -= HandleStageProgressChanged;
			}
		}
		#endregion


		#region EVENT HANDLERS
		void HandleStageProgressChanged(StageProgress progress)
		{
			onStageProgressChanged(this, progress);
		}
		#endregion


		#region PUBLIC API
		public void StartStage()
		{
			stageBehaviour.StartBehavior();
		}
		#endregion
	}
}