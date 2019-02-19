using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Players
{
	/// <summary>
	/// Represents a player.
	/// </summary>
	public class Player
	{
		#region PROPERTIES
		/// The index assigned to this player.
		/// This corresponds to the input controller index.
		public int playerIndex { get; private set; }

		/// A spawned instance of this player.
		public DerbyCar playerInstance { get; set; }
		#endregion


		#region CONSTRUCTORS
		public Player(int index)
		{
			playerIndex = index;
		}
		#endregion
	}
}