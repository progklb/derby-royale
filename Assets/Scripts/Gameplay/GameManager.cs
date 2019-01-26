using UnityEngine;

using System;
using System.Collections;

using DerbyRoyale.Levels;
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
		#endregion


		#region VARIABLES
		public int numberOfPlayers { get; private set; }

		public DerbyCar playerInstance { get; private set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private DerbyCar m_PlayerCarPrefab;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			//TODO DerbyCar += HandleLevelCompleted;
		}

		void OnDestroy()
		{
			//TODO DerbyCar -= HandleLevelCompleted; 
		}
		#endregion


		#region EVENT HANDLERS
		void HandlePlayerDeath(DerbyCar player)
		{
			numberOfPlayers--;

			// If player has died
			if (player == playerInstance)
			{
				onGameOver(numberOfPlayers == 0 ? GameOverCondition.Survived : GameOverCondition.Died);
			}
		}
		#endregion


		#region PUBLIC API
		public void StartGame()
		{
			StartCoroutine(StartGameSequence());
		}

		public void EndGame()
		{
			Destroy(playerInstance.gameObject);
			playerInstance = null;

			SceneManager.instance.UnloadScene(SceneType.GameScene);
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator StartGameSequence()
		{
			SceneManager.instance.LoadScene(SceneType.GameScene);

			// Wait until the scene is fully loaded and the level controller been set up.
			yield return new WaitUntil(() => LevelManager.instance.currentController != null);

			// Spawn player instance

			var randomIdx = URandom.Range(0, levelController.currentStage.spawnPoints.Length);
			var spawnPoint = levelController.currentStage.spawnPoints[randomIdx];

			playerInstance = Instantiate(m_PlayerCarPrefab, spawnPoint.position, spawnPoint.rotation, transform);

			onGameStart();

			// HACK. Attach camera to player.
			var cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
			cam.Follow = playerInstance.transform;
			cam.LookAt = playerInstance.transform;
		}
		#endregion
	}
}