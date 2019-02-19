using UnityEngine;

using System;
using System.Collections;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Input
{
	/// <summary>
	/// Monitors connected joysticks for connect/disconnect and notifies when changes occur.
	/// </summary>
	public class JoystickMonitor : MonoBehaviour
	{
		#region EVENTS
		// TODO Make this a proper delegate type.
		/// Raised when the connect status of a joystick changes. 
		/// First param: joystick number. Second param: joystick name. Third param: connected
		public Action<int, string, bool> onJoystickConnectionChanged = delegate { };
		#endregion


		#region PROPERTIES
		/// Number of connected joystick controllers that are currently active.
		public int connectedJoysticks { get; private set; }

		/// Unity's joystick list.
		private string[] joystickNames { get => UInput.GetJoystickNames(); }
		/// A cache of Unity's joystick list from the previous frame, for detecting connection changes.
		private string[] joystickNamesCache { get; set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] float m_JoystickCheckInterval = 2f;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			StartCoroutine(UpdateJoysticksSequence());
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator UpdateJoysticksSequence()
		{
			joystickNamesCache = new string[joystickNames.Length];

			while (true)
			{
				connectedJoysticks = 0;

				if (joystickNames.Length > 0)
				{
					for (int i = 0; i < joystickNames.Length; ++i)
					{
						if (!string.IsNullOrEmpty(joystickNames[i]))
						{
							connectedJoysticks++;

							if (string.IsNullOrEmpty(joystickNamesCache[i]))
							{
								Debug.Log("Controller " + i + " is connected using: " + joystickNames[i]);
								onJoystickConnectionChanged(i, joystickNames[i], true);
							}
						}
						else if (string.IsNullOrEmpty(joystickNames[i]) && !string.IsNullOrEmpty(joystickNamesCache[i]))
						{
							Debug.Log("Controller: " + i + " is disconnected.");
							onJoystickConnectionChanged(i, joystickNamesCache[i], false);
						}
					}

					joystickNamesCache = joystickNames.Clone() as string[];
				}

				yield return new WaitForSeconds(m_JoystickCheckInterval);
			}
		}
		#endregion
	}
}