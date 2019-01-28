using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using DerbyRoyale.Levels;
using DerbyRoyale.Players;
using DerbyRoyale.Scenes;
using DerbyRoyale.Vehicles;

using URandom = UnityEngine.Random;

namespace DerbyRoyale.Gameplay
{
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
		public LevelController levelController { get => LevelManager.instance?.currentController; }

		public int playerCount { get => currentPlayers.Count; }
		public Dictionary<int, Player> currentPlayers { get; private set; } = new Dictionary<int, Player>();

		// TODO What to do here?
		public DerbyCar playerInstance { get; private set; }

		/// Indicates whether a game is active or not.
		public bool isRunning { get; private set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private DerbyCar m_PlayerCarPrefab;
		#endregion


		#region UNITY EVENTS
		protected override void Awake()
		{
			base.Awake();

			LocalPlayerConnect.onPlayerConnect += HandlePlayerConnect;
			DerbyCar.onSpawn += HandlePlayerSpawn;
			DerbyCar.onDeath += HandlePlayerDeath;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			LocalPlayerConnect.onPlayerDisconnect -= HandlePlayerDisconnect;
			DerbyCar.onSpawn -= HandlePlayerSpawn;
			DerbyCar.onDeath -= HandlePlayerDeath;
		}
		#endregion


		#region EVENT HANDLERS
		void HandlePlayerConnect(int playerIndex)
		{
			if (!currentPlayers.ContainsKey(playerIndex))
			{
				Debug.LogError("Connecting to game: " + playerIndex);
				currentPlayers.Add(playerIndex, new Player(playerIndex));
			}
		}

		void HandlePlayerDisconnect(int playerIndex)
		{
			if (currentPlayers.ContainsKey(playerIndex))
			{
				Debug.LogError("Disconnecting from game: " + playerIndex);
				currentPlayers.Remove(playerIndex);
				// TODO Despawn
			}
		}

		void HandlePlayerSpawn(DerbyCar player)
		{
			//numberOfPlayers++;
		}

		void HandlePlayerDeath(DerbyCar player)
		{
			//numberOfPlayers--;

			// REFACTOR
			// If the player has died, check whether we were the last survivor on the map.
			if (player == playerInstance)
			{
				//onGameOver(numberOfPlayers == 0 ? GameOverCondition.LastSurvivor : GameOverCondition.Died);
			}
			// If another player has died, check if we were the last survivor
			//else if (numberOfPlayers == 1)
			{
				onGameOver(GameOverCondition.LastSurvivor);
			}
		}
		#endregion


		#region PUBLIC API
		public void StartGame()
		{
			if (playerCount < 2)
			{
				Debug.LogError("Cannot start game with less than 2 players.");
				//return;
			}

			StartCoroutine(StartGameSequence());
		}

		public void EndGame()
		{
			ResetManager();

			SceneManager.instance.UnloadScene(SceneType.GameScene);

			isRunning = false;
			onGameEnd();
		}
		#endregion


		#region HELPER FUNCTIONS
		void ResetManager()
		{
			if (playerInstance != null)
			{
				Destroy(playerInstance.gameObject);
			}

			playerInstance = null;

			currentPlayers.Clear();
		}

		void SpawnPlayer()
		{
			var randomIdx = URandom.Range(0, levelController.currentStage.spawnPoints.Length);
			var spawnPoint = levelController.currentStage.spawnPoints[randomIdx];

			playerInstance = Instantiate(m_PlayerCarPrefab, spawnPoint.position, spawnPoint.rotation, transform);
		}

		IEnumerator StartGameSequence()
		{
			SceneManager.instance.LoadScene(SceneType.GameScene);

			// Wait until the scene is fully loaded and the level controller been set up.
			yield return new WaitUntil(() => LevelManager.instance.currentController != null);

			// TODO Spawn player instances.
			SpawnPlayer();

			isRunning = true;
			onGameStart();
		}
		#endregion
	}
}