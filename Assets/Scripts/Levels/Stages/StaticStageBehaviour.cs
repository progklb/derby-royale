using UnityEngine;

using System.Collections;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// A stage specifically designed to do nothing.
	/// </summary>
	public class StaticStageBehaviour : StageBehaviour
	{
		#region PUBLIC API
		public override IEnumerator StartBehaviourSequence()
		{
			yield return null;
		}
		#endregion
	}
}