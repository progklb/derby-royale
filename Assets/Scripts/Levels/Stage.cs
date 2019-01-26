using UnityEngine;

using System;
using System.Collections;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Represents a single stage (tier) of a level.
	/// </summary>
	public class Stage : MonoBehaviour
	{
		#region EVENTS
		public static event Action<Stage> onStageStart = delegate { };
		public static event Action<Stage> onStageCompleteBegin = delegate { };
		public static event Action<Stage> onStageCompleteEnd = delegate { };
		public static event Action<Stage> onStageDestroyed = delegate { };
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


		#region CONSTANTS
		public const string STAGE_COMPLETE_TRIGGER = "StageComplete";
		public const string STAGE_DESTROY_TRIGGER = "StageDestroy";
		#endregion


		#region PROPERTIES
		/// Defines the parameters this stage.
		public StageParameters stageParameter { get => m_StageParameters; }

		public int stageNumber { get; set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private StageParameters m_StageParameters;
		[SerializeField] private Animator m_Animator;
		#endregion


		#region PUBLIC API
		public void StartStage()
		{
			StartCoroutine(StartStageSequence());
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator StartStageSequence()
		{
			Debug.Log($"Starting stage {stageNumber}");
			onStageStart(this);
			yield return new WaitForSeconds(stageParameter.stageTimeout);

			Debug.Log($"Completing stage {stageNumber}");
			onStageCompleteBegin(this);
			m_Animator.SetTrigger(STAGE_COMPLETE_TRIGGER);
			yield return new WaitForSeconds(stageParameter.stageCompleteTimeout);
			onStageCompleteEnd(this);

			Debug.Log($"Stage {stageNumber} completed. Begining detroy timeout.");
			yield return new WaitForSeconds(stageParameter.stageDestroyTimeout);
			m_Animator.SetTrigger(STAGE_DESTROY_TRIGGER);
			onStageDestroyed(this);
			Debug.Log($"Stage {stageNumber} destroyed.");

			// Clean up game objects
			yield return new WaitForSeconds(10f);
			Destroy(gameObject);
		}
		#endregion
	}
}