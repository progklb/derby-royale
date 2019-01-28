using UnityEngine;

using System;

using DerbyRoyale.Input;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Gameplay
{
	public class LocalPlayerConnect : MonoBehaviour
	{
		#region EVENTS
		public static event Action<int> onPlayerConnect = delegate { };
		public static event Action<int> onPlayerDisconnect = delegate { };
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			InputManager.onDeviceConnectionChanged += HandleDeviceConnectionChanged;
		}

		void Update()
		{
			for (int i = 0; i < InputManager.instance.connectedDeviceCount && i < InputManager.instance.maxLocalPlayers; ++i)
			{
				//Debug.Log($"Checking axis:  Join_J{i + 1} : " + UInput.GetAxis($"Join_J{i + 1}"));
				if (UInput.GetAxis($"Join_J{i + 1}") > 0.5f)
				{
					onPlayerConnect(i + 1);
				}
			}
		}

		void OnDestroy()
		{
			InputManager.onDeviceConnectionChanged -= HandleDeviceConnectionChanged;
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