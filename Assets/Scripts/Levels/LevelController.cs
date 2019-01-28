using UnityEngine;

using DerbyRoyale.Gameplay;

using URandom = UnityEngine.Random;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Controls a level with multiple stages.
	/// </summary>
	public class LevelController : MonoBehaviour
	{
		#region PROPERTIES
		public int currentStageIndex { get; private set; }
		public Stage currentStage { get => m_Stages[currentStageIndex]; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private Stage[] m_Stages;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			LevelManager.instance.RegisterController(this);

			Stage.onStageProgressChanged += HandleStageProgressChanged;

			for (int i = 0; i < m_Stages.Length; ++i)
			{
				m_Stages[i].stageNumber = i + 1;
			}

			if (m_Stages.Length > 0)
			{
				currentStage.StartStage();
			}
			else
			{
				Debug.LogWarning("There are no stages assigned to this level controller.");
			}
		}

		void OnDestroy()
		{
			LevelManager.instance?.DeregisterController(this);

			Stage.onStageProgressChanged -= HandleStageProgressChanged;
		}
		#endregion


		#region HELPER FUNCTIONS
		void HandleStageProgressChanged(Stage stage, StageProgress progress)
		{
			// If the stage that has ended is our current stage
			if (currentStage == stage && progress == StageProgress.CompletedEnd)
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