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
	/// <summary>
	/// A controller for local multiplayer gameplay types.
	/// </summary>
	[RequireComponent(typeof(LocalPlayerConnect))]
	public class LocalMultiplayerGameController : GameController
	{
		#region PROPERTIES
		public int playerCount { get => currentPlayers.Count; }
		public Dictionary<int, Player> currentPlayers { get; private set; } = new Dictionary<int, Player>();

		// TODO Need a more final solution. Suggest camera rig to be spawned with each player to offer dynamic split-screen.
		public DerbyCar playerInstance
		{
			get => GameManager.instance.playerInstance;
			set => GameManager.instance.playerInstance = value;
		}

		public int maxLocalPlayers { get => m_MaxLocalPlayers; }

		private LocalPlayerConnect localPlayerConnect { get; set; }
		#endregion


		#region EDITOR FIELDS
		[Range(1, 8)]
		[SerializeField] private int m_MaxLocalPlayers = 2;
		#endregion


		#region UNITY EVENTS
		protected void Awake()
		{
			localPlayerConnect = GetComponent<LocalPlayerConnect>();
			localPlayerConnect.isScanning = true;

			LocalPlayerConnect.onPlayerConnect += HandlePlayerConnect;

			DerbyCar.onSpawn += HandlePlayerSpawn;
			DerbyCar.onDeath += HandlePlayerDeath;
		}

		protected void OnDestroy()
		{
			LocalPlayerConnect.onPlayerDisconnect -= HandlePlayerDisconnect;

			DerbyCar.onSpawn -= HandlePlayerSpawn;
			DerbyCar.onDeath -= HandlePlayerDeath;
		}
		#endregion


		#region EVENT HANDLERS
		protected override void HandlePlayerConnect(int playerIndex)
		{
			if (!currentPlayers.ContainsKey(playerIndex))
			{
				Debug.Log($"Connecting to game: player ID {playerIndex}");
				currentPlayers.Add(playerIndex, new Player(playerIndex));

				// Disable scanning for input for players joining if number max players have joined.
				localPlayerConnect.isScanning = playerCount < maxLocalPlayers;

				if (playerCount > 0 && gameState != GameState.Playing)
				{
					gameManager.StartGame();
				}
			}
		}

		protected override void HandlePlayerDisconnect(int playerIndex)
		{
			if (currentPlayers.ContainsKey(playerIndex))
			{
				Debug.Log($"Disconnecting from game: player ID  {playerIndex}");
				currentPlayers.Remove(playerIndex);

				// Reenable scanning for input for players joining 
				localPlayerConnect.isScanning = true;

				// TODO Despawn. Check if we are the last player and handle leaving the game if so.
			}
		}

		protected override void HandlePlayerSpawn(DerbyCar player)
		{
			//numberOfPlayers++;
		}

		protected override void HandlePlayerDeath(DerbyCar player)
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
				RaiseOnGameOver(GameOverCondition.LastSurvivor);
			}
		}
		#endregion


		#region PUBLIC API
		public override void StartGame()
		{
			if (playerCount < 2)
			{
				Debug.LogError("Cannot start game with less than 2 players.");
				//return;
			}

			StartCoroutine(StartGameSequence());

			base.StartGame();
		}

		public override void EndGame()
		{
			Reset();

			SceneManager.instance.UnloadScene(SceneType.GameScene);

			base.EndGame();
		}

		public override void Reset()
		{
			base.Reset();

			// TODO Remove playerInstance
			if (playerInstance != null)
			{
				Destroy(playerInstance.gameObject);
			}

			playerInstance = null;

			currentPlayers.Clear();
		}
		#endregion


		#region HELPER FUNCTIONS
		void SpawnPlayer()
		{
			var randomIdx = URandom.Range(0, levelController.currentStage.spawnPoints.Length);
			var spawnPoint = levelController.currentStage.spawnPoints[randomIdx];

			playerInstance = Instantiate(GameManager.instance.playerPrefab, spawnPoint.position, spawnPoint.rotation, transform);
		}

		IEnumerator StartGameSequence()
		{
			SceneManager.instance.LoadScene(SceneType.GameScene);

			// Wait until the scene is fully loaded and the level controller been set up.
			yield return new WaitUntil(() => LevelManager.instance.currentController != null);

			// TODO Spawn all player instances.
			SpawnPlayer();
		}
		#endregion
	}
}