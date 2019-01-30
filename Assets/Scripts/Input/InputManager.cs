using UnityEngine;

using DerbyRoyale.Utilities;

namespace DerbyRoyale.Input
{
	[RequireComponent(typeof(JoystickMonitor))]
	public class InputManager : Manager<InputManager>
	{
		#region EVENTS
		public delegate void JoystickConnectionChanged(int index, bool connected);

		/// Raised when a joystick device is connected or disconnected.
		public static event JoystickConnectionChanged onJoystickConnectionChanged;
		#endregion

		#region PROPERTIES
		private JoystickMonitor joystickMonitor { get; set; }

		public int connectedJoysticksCount { get => joystickMonitor.connectedJoysticks; }
		#endregion


		#region UNITY EVENTS
		protected override void Awake()
		{
			base.Awake();

			joystickMonitor = GetComponent<JoystickMonitor>();
			joystickMonitor.onJoystickConnectionChanged += HandleJoystickConnectionChanged;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			joystickMonitor.onJoystickConnectionChanged -= HandleJoystickConnectionChanged;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleJoystickConnectionChanged(int idx, string displayName, bool connected)
		{
			onJoystickConnectionChanged(idx, connected);
		}
		#endregion


		#region PUBLIC API
		public static string GetAxisName(InputType type, InputSet set)
		{
			return string.Format($"{type.GetDescription()}{set.GetDescription()}");
		}
		#endregion
	}
}