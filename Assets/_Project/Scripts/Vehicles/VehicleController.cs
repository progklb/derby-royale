using UnityEngine;

using DerbyRoyale.Input;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Vehicles
{
    [AddComponentMenu("Derby Royale/Vehicles/Vehicle Controller")]
	[RequireComponent(typeof(Vehicle))]
    public class VehicleController : MonoBehaviour
    {
		#region VARIABLES
		public Vehicle vehicle { get; private set; }

		public float acceleration { get => vehicle?.player?.GetInput(InputType.Vertical) ?? 0f; }
        public float turning { get => vehicle?.player?.GetInput(InputType.Horizontal) ?? 0f; }

        public bool isAccelerating { get => acceleration != 0f; }
        public bool isTurning { get => turning != 0f; }
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			vehicle = GetComponent<Vehicle>();
		}
		#endregion
	}
}