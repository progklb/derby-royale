using UnityEngine;

using System;

using DerbyRoyale.Levels;
using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Gameplay
{
	public class GameManager : MonoBehaviour
	{
		#region EVENTS
		public static event Action<GameOverCondition> onGameOver;
		#endregion


		#region VARIABLES
		public int numberOfPlayers { get; private set; }

		public DerbyCar playerCarInstance { get; set; }
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
			if (player == playerCarInstance)
			{
				onGameOver(numberOfPlayers == 0 ? GameOverCondition.Survived : GameOverCondition.Died);
			}
		}
		#endregion


		#region PUBLIC API
		public void StartGame()
		{

		}

		public void EndGame()
		{

		}
		#endregion
	}
}