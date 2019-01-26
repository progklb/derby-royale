using UnityEngine;

using System.Collections;

namespace DerbyRoyale.Levels
{
	public class FinalStageBehaviour : StageBehaviour
	{
		#region CONSTANTS
		public const string STAGE_BEGIN_TRIGGER = "StageBegin";
		#endregion


		#region PUBLIC API
		public override IEnumerator StartBehaviourSequence()
		{
			Debug.Log($"Starting stage {stage.stageNumber}");

			animator.SetTrigger(STAGE_BEGIN_TRIGGER);
			RaiseStageProgressChanged(StageProgress.Begin);
			yield return new WaitForSeconds(stage.parameters.stageTimeout);

		}
		#endregion
	}
}