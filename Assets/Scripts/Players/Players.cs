using UnityEngine;

namespace DerbyRoyale.Players
{
	/// <summary>
	/// Represents a player.
	/// </summary>
	public class Player : MonoBehaviour
	{
		#region PROPERTIES
		public int playerIndex { get; private set; }
		#endregion


		#region CONSTRUCTORS
		public Player(int index)
		{
			playerIndex = index;
		}
		#endregion
	}
}