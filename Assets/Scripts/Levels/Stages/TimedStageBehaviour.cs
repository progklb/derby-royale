using UnityEngine;

using System;
using System.Collections;

namespace DerbyRoyale.Levels
{
	public class TimedStageBehaviour : StageBehaviour
	{
		#region TYPES
		[Serializable]
		public class TimedStageParameters
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
		public const string STAGE_BEGIN_TRIGGER = "StageBegin";
		public const string STAGE_COMPLETE_TRIGGER = "StageComplete";
		public const string STAGE_DESTROY_TRIGGER = "StageDestroy";
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private TimedStageParameters m_Parameters;
		#endregion


		#region PUBLIC API
		public override IEnumerator StartBehaviourSequence()
		{
			Debug.Log($"Starting stage {stage.stageNumber}");

			animator.SetTrigger(STAGE_BEGIN_TRIGGER);
			RaiseStageProgressChanged(StageProgress.Begin);
			yield return new WaitForSeconds(m_Parameters.stageTimeout);

			Debug.Log($"Completing stage {stage.stageNumber}");

			animator.SetTrigger(STAGE_COMPLETE_TRIGGER);
			RaiseStageProgressChanged(StageProgress.CompletedBegin);
			yield return new WaitForSeconds(m_Parameters.stageCompleteTimeout);
			RaiseStageProgressChanged(StageProgress.CompletedEnd);

			Debug.Log($"Stage {stage.stageNumber} completed. Begining detroy timeout.");

			yield return new WaitForSeconds(m_Parameters.stageDestroyTimeout);
			animator.SetTrigger(STAGE_DESTROY_TRIGGER);
			RaiseStageProgressChanged(StageProgress.Destroyed);

			Debug.Log($"Stage {stage.stageNumber} destroyed.");

			// Clean up game objects
			yield return new WaitForSeconds(10f);
			Destroy(gameObject);
		}
		#endregion
	}
}