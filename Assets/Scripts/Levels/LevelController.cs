using UnityEngine;

using System;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Controls a level with multiple stages.
	/// </summary>
	public class LevelController : MonoBehaviour
	{
		#region EVENT
		public static event Action onLevelCompleted = delegate { };
		#endregion


		#region PROPERTIES
		public Stage currentStage { get => m_Stages[currentStageIndex]; }
		public int currentStageIndex { get; private set; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private Stage[] m_Stages;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			Stage.onStageCompleteEnd += HandleStageCompleted;

			// TODO Have a way of listening for house occupation.


			for (int i = 0; i < m_Stages.Length; ++i)
			{
				m_Stages[i].stageNumber = i + 1;
			}

			currentStage.StartStage();
		}

		void OnDestroy()
		{
			Stage.onStageCompleteEnd -= HandleStageCompleted;
		}
		#endregion


		#region HELPER FUNCTIONS
		void HandleStageCompleted(Stage stage)
		{
			// If the stage that has ended is our current stage
			if (currentStage == stage)
			{
				// And there are further stages to complete, then advance to the next state.
				if (currentStageIndex < m_Stages.Length - 1)
				{
					currentStageIndex++;
					StartStage(currentStage);
				}
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void StartStage(Stage stage)
		{
			stage.StartStage();
		}
		#endregion
	}
}