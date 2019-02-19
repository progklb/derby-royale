using UnityEngine;

using DerbyRoyale.Input;
using DerbyRoyale.Vehicles;

using UInput = UnityEngine.Input;

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
		public Vehicle vehicleInstance { get; private set; }
		#endregion


		#region CONSTRUCTORS
		public Player(int index)
		{
			playerIndex = index;
		}
		#endregion


		#region PUBLIC  API
		public void SetVehicleInstance(Vehicle vehicle)
		{
			vehicleInstance = vehicle;
			vehicle.Initialise(this);
		}

		public void ClearVehicleInstance()
		{
			vehicleInstance = null;
		}

		public float GetInput(InputType type)
		{
			return UInput.GetAxis(InputManager.GetAxisName(type, (InputSet)playerIndex));
		}
		#endregion
	}
}