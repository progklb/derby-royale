using UnityEngine;

using System.Collections;

namespace DerbyRoyale.Levels
{
	public class FinalStageBehaviour : StageBehaviour
	{
		public override IEnumerator StartBehaviourSequence()
		{
			yield return null;
			Debug.Log("Final stage!");
		}
	}
}