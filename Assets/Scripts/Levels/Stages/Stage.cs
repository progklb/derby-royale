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


		#region PROPERTIES
		public int stageNumber { get; set; }
		public Transform[] spawnPoints { get => m_SpawnPoints; }


		private StageBehaviour stageBehaviour { get; set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private Transform[] m_SpawnPoints;
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

		void OnDisable()
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