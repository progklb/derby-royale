using System.Collections;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Interface for implementation into behaviours for stages.
	/// </summary>
	public interface IStageBehaviour
	{
		void StartBehavior();
		IEnumerator StartBehaviourSequence();
	}
}