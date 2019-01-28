using UnityEngine;

using System;

using DerbyRoyale.Levels;
using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Gameplay
{
	/// <summary>
	/// Serves as a base class for defining different game modes.
	/// </summary>
	public abstract class GameController : MonoBehaviour
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
		public GameManager gameManager { get => GameManager.instance; }

		public LevelController levelController { get => LevelManager.instance?.currentController; }

		public GameState gameState { get; protected set; }
		#endregion


		#region EVENT HANDLERS
		protected virtual void HandlePlayerConnect() { }

		protected virtual void HandlePlayerConnect(int playerIndex) { }

		protected virtual void HandlePlayerDisconnect(int playerIndex) { }

		// TODO Change these for Player objects.

		protected virtual void HandlePlayerSpawn(DerbyCar player) { }

		protected virtual void HandlePlayerDeath(DerbyCar player)  { }
		#endregion


		#region PUBLIC API
		public virtual void StartGame()
		{
			gameState = GameState.Playing;
			onGameStart();
		}

		public virtual void EndGame()
		{
			gameState = GameState.Stopped;
			onGameEnd();
		}

		public virtual void Reset() { }
		#endregion


		#region HELPER FUNCTIONS
		protected void RaiseOnGameOver(GameOverCondition condition)
		{
			onGameOver(condition);
		}
		#endregion
	}
}