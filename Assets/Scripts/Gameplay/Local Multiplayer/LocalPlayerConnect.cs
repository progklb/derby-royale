using UnityEngine;

using System;

using DerbyRoyale.Input;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Gameplay
{
	public class LocalPlayerConnect : MonoBehaviour
	{
		#region EVENTS
		public delegate void PlayerConnectionChanged(int playerIndex);

		public static event PlayerConnectionChanged onPlayerConnect = delegate { };
		public static event PlayerConnectionChanged onPlayerDisconnect = delegate { };
		#endregion


		#region PROPERTIES
		/// If true, input from all input sources will be checked for any player's waiting to join.
		public bool isScanning { get; set; }
		
		/// A cache of all input set appendices to check against for input.
		private InputSet[] inputSetList { get; set; }
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			inputSetList = (InputSet[])Enum.GetValues(typeof(InputSet));

			InputManager.onJoystickConnectionChanged += HandleDeviceConnectionChanged;
		}

		void Update()
		{
			if (isScanning)
			{
				for (int i = 0; i < inputSetList.Length; ++i)
				{
					var axisName = InputManager.GetAxisName(InputType.Fire, inputSetList[i]);

					if (UInput.GetAxis(axisName) > 0.1f)
					{
						onPlayerConnect(i + 1);
					}
				}
			}
		}

		void OnDestroy()
		{
			InputManager.onJoystickConnectionChanged -= HandleDeviceConnectionChanged;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleDeviceConnectionChanged(int idx, bool connected)
		{
			if (!connected)
			{
				Debug.LogError(" Disconnecting game: " + idx);

				onPlayerDisconnect(idx);
			}
		}
		#endregion
	}
}