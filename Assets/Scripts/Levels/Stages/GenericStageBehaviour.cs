using UnityEngine;

using System.Collections;

namespace DerbyRoyale.Levels
{
	public class GenericStageBehaviour : StageBehaviour
	{
		#region CONSTANTS
		public const string STAGE_BEGIN_TRIGGER = "StageBegin";
		public const string STAGE_COMPLETE_TRIGGER = "StageComplete";
		public const string STAGE_DESTROY_TRIGGER = "StageDestroy";
		#endregion


		#region PUBLIC API
		public override IEnumerator StartBehaviourSequence()
		{
			Debug.Log($"Starting stage {stage.stageNumber}");

			animator.SetTrigger(STAGE_BEGIN_TRIGGER);
			RaiseStageProgressChanged(StageProgress.Begin);
			yield return new WaitForSeconds(stage.parameters.stageTimeout);

			Debug.Log($"Completing stage {stage.stageNumber}");

			animator.SetTrigger(STAGE_COMPLETE_TRIGGER);
			RaiseStageProgressChanged(StageProgress.CompletedBegin);
			yield return new WaitForSeconds(stage.parameters.stageCompleteTimeout);
			RaiseStageProgressChanged(StageProgress.CompletedEnd);

			Debug.Log($"Stage {stage.stageNumber} completed. Begining detroy timeout.");

			yield return new WaitForSeconds(stage.parameters.stageDestroyTimeout);
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