using UnityEngine;

using System;

using DerbyRoyale.Levels;
using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Gameplay
{
	/// <summary>
	/// Top-level manager for accessing and controlling the game state.
	/// </summary>
	public class GameManager : Manager<GameManager>
	{
		#region EVENTS
		/// Raised when the game is loaded.
		public static event Action onGameStart = delegate { };
		/// Raised when the game is unloaded.
		public static event Action onGameEnd = delegate { };

		/// Raised when the game ends due to gameplay conditions.
		public static event Action<GameOverCondition> onGameOver = delegate { };
		#endregion


		#region PROPERTIES
		public GameController gameController { get => m_GameController; }
		public LevelController levelController { get => LevelManager.instance?.currentController; }

		public GameState gameState { get => gameController?.gameState ?? GameState.Stopped; }

		public DerbyCar playerPrefab { get => m_PlayerPrefab; }

		// TODO REFACTOR This is for prototyping purposes.
		public DerbyCar playerInstance { get; set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private DerbyCar m_PlayerPrefab;

		// TODO Develop final solution.
		[Header("For Development")]
		[SerializeField] private GameController m_GameController;
		#endregion


		#region PUBLIC API
		public void StartGame()
		{
			if (gameController != null)
			{
				gameController.StartGame();
			}
			else
			{
				Debug.LogError("Cannot start game without a loaded controller.");
			}
		}

		public void EndGame()
		{
			if (gameController != null)
			{
				gameController.EndGame();
			}
			else
			{
				Debug.LogError("Cannot start end without a loaded controller.");
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void Reset()
		{
			gameController?.Reset();
		}
		#endregion
	}
}