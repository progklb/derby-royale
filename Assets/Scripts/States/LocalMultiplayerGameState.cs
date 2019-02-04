using UnityEngine;

using Utilities.StateMachine;

using DerbyRoyale.Gameplay;

namespace DerbyRoyale
{
	public class LocalMultiplayerGameState : State
	{
		#region VARIABLES
		[SerializeField] private GameObject m_GameUI;
		#endregion


		#region OVERRIDES
		protected override void OnEnable()
		{
			base.OnEnable();

			m_GameUI.SetActive(true);

			GameController.onGameEnd += HandleGameEnd;
			GameController.onGameOver += HandleGameOver;
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			m_GameUI.SetActive(false);

			GameController.onGameEnd -= HandleGameEnd;
			GameController.onGameOver -= HandleGameOver;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleGameEnd()
		{
			NextState();
		}

		void HandleGameOver(GameOverCondition condition)
		{
			NextState();
		}
		#endregion
	}
}