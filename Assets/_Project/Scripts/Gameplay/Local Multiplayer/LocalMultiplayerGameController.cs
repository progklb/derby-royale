using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using DerbyRoyale.Cameras;
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

		public int maxLocalPlayers { get => m_MaxLocalPlayers; }

		private LocalPlayerConnect localPlayerConnect { get; set; }
		#endregion


		#region EDITOR FIELDS
		[Range(1, 4)]
		[SerializeField] private int m_MaxLocalPlayers = 2;
		#endregion


		#region UNITY EVENTS
		protected void Awake()
		{
			localPlayerConnect = GetComponent<LocalPlayerConnect>();
			localPlayerConnect.isScanning = true;

			LocalPlayerConnect.onPlayerConnect += HandlePlayerConnect;
			LocalPlayerConnect.onPlayerDisconnect += HandlePlayerDisconnect;

			DerbyCar.onSpawn += HandlePlayerSpawn;
			DerbyCar.onDeath += HandlePlayerDeath;
		}

		protected void OnDestroy()
		{
			LocalPlayerConnect.onPlayerConnect -= HandlePlayerConnect;
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
				var player = new Player(playerIndex);
				currentPlayers.Add(playerIndex, player);

				if (gameState != GameState.Playing)
				{
					gameManager.StartGame();
				}
				else
				{
					SpawnPlayer(player);
				}

				// Disable scanning for input for players joining if number max players have joined.
				localPlayerConnect.isScanning = playerCount < maxLocalPlayers;
			}
		}

		protected override void HandlePlayerDisconnect(int playerIndex)
		{
			if (currentPlayers.ContainsKey(playerIndex))
			{
				if (gameState == GameState.Playing)
				{
					Despawn(currentPlayers[playerIndex]);
				}

				Debug.Log($"Disconnecting from game: player ID  {playerIndex}");
				currentPlayers.Remove(playerIndex);

				// Reenable scanning for input for players joining 
				localPlayerConnect.isScanning = true;

				

				if (playerCount == 0)
				{
					RaiseOnGameEnd();
				}
			}
		}

		protected override void HandlePlayerSpawn(DerbyCar player)
		{
		}

		protected override void HandlePlayerDeath(DerbyCar player)
		{
			// TODO Handle
			RaiseOnGameOver(GameOverCondition.LastSurvivor);
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

			foreach (var player in currentPlayers.Values)
			{
				Despawn(player);
			}

			currentPlayers.Clear();
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator StartGameSequence()
		{
			SceneManager.instance.LoadScene(SceneType.GameScene);

			// Wait until the scene is fully loaded and the level controller been set up.
			yield return new WaitUntil(() => LevelManager.instance.currentController != null);

			foreach (var player in currentPlayers.Values)
			{
				SpawnPlayer(player);
			}
		}

		void SpawnPlayer(Player player)
		{
			var randomIdx = URandom.Range(0, levelController.currentStage.spawnPoints.Length);
			var spawnPoint = levelController.currentStage.spawnPoints[randomIdx];

			player.playerInstance = Instantiate(GameManager.instance.playerPrefab, spawnPoint.position, spawnPoint.rotation, transform);

			CameraManager.instance.Add(player);
		}
		
		void Despawn(Player player)
		{
			CameraManager.instance.Remove(player);

			Destroy(player.playerInstance.gameObject);
		}
		#endregion
	}
}