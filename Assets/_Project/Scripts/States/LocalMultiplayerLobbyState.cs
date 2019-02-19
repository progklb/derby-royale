using UnityEngine;

using Utilities.StateMachine;

using DerbyRoyale.Gameplay;

namespace DerbyRoyale
{
	public class LocalMultiplayerLobbyState : State
	{
		#region EDITOR FIELDS
		[SerializeField] private GameObject m_LobbyUI;
		#endregion


		#region OVERRIDES
		protected override void OnEnable()
		{
			base.OnEnable();

			m_LobbyUI.SetActive(true);

			GameController.onGameStart += HandleGameStart;
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			m_LobbyUI.SetActive(false);

			GameController.onGameStart -= HandleGameStart;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleGameStart()
		{
			NextState();
		}
		#endregion
	}
}