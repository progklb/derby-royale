using UnityEngine;

using System.Collections;

using DerbyRoyale.Gameplay;

namespace DerbyRoyale.UI
{
	/// <summary>
	/// Logic for the game over screen.
	/// </summary>
	public class UIGameOver : MonoBehaviour
	{
		#region EDITOR FIELDS
		[SerializeField] private GameObject m_Parent;
		[Space]
		[SerializeField] private GameObject m_DeathElement;
		[SerializeField] private GameObject m_SurvivorElement;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			GameManager.onGameOver += HandleGameOver;
		}

		void OnDestroy()
		{
			GameManager.onGameOver -= HandleGameOver;
		}
		#endregion


		#region EVENT HANDLERS
		void HandleGameOver(GameOverCondition condition)
		{
			StartCoroutine(DisplayGameOverSequence(condition));
		}
		#endregion


		#region PUBLIC API
		// TODO This should be more like a reponder, listening for game over, and begining a sequence of actions.
		// A game over state machine is warranted here.

		IEnumerator DisplayGameOverSequence(GameOverCondition condition)
		{
			Time.timeScale = 0.3f;
			yield return new WaitForSecondsRealtime(3f);
			Time.timeScale = 1f;

			m_Parent.SetActive(true);
			m_DeathElement.SetActive(false);
			m_SurvivorElement.SetActive(false);

			switch (condition)
			{
				case GameOverCondition.LastSurvivor:
					m_SurvivorElement.SetActive(true);
					break;

				case GameOverCondition.Died:
					m_DeathElement.SetActive(true);
					break;
			}
		}
		#endregion
	}
}
