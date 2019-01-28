using UnityEngine;

using System;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Input
{
	[RequireComponent(typeof(JoystickMonitor))]
	public class InputManager : Manager<InputManager>
	{
		// Possible input sets:
		// Controller (Start)
		// WASD, IJKL, arrows, numpad

		#region EVENTS
		public static event Action<int, bool> onDeviceConnectionChanged = delegate { };
		#endregion

		#region PROPERTIES
		private JoystickMonitor joystickMonitor { get; set; }

		public int maxLocalPlayers { get => m_MaxLocalPlayers; }
		// TODO Cater for keyboards
		public int connectedDeviceCount { get => joystickMonitor.connectedJoysticks; }
		#endregion


		#region EDITOR FIELDS
		[Min(1)]
		[SerializeField] private int m_MaxLocalPlayers;
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
			onDeviceConnectionChanged(idx, connected);
		}
		#endregion
	}
}