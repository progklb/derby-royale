using UnityEngine;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Input
{
	[RequireComponent(typeof(JoystickMonitor))]
	public class InputManager : Manager<InputManager>
	{
		// Possible input sets:
		// Controller (Start)
		// WASD, IJKL, arrows, numpad

		#region EDITOR FIELDS
		private JoystickMonitor joystickMonitor;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			joystickMonitor = GetComponent<JoystickMonitor>();
			joystickMonitor.onJoystickConnectionChanged += HandleJoystickConnectionChanged;
		}

		void OnDestroy()
		{
			joystickMonitor.onJoystickConnectionChanged -= HandleJoystickConnectionChanged;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleJoystickConnectionChanged(int idx, string displayName, bool connected)
		{
		}
		#endregion
	}
}