using UnityEngine;

using System;
using System.Collections;

namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Base class for defining behaviour sequences for a <see cref="Stage"/>.
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Stage))]
	public abstract class StageBehaviour : MonoBehaviour, IStageBehaviour
	{
		#region EVENTS
		public event Action<StageProgress> onStageProgressChanged = delegate { };
		#endregion


		#region PROPERTIES
		public Stage stage { get; private set; }
		public Animator animator { get => m_Animator; }
		#endregion


		#region VARIABLES
		[SerializeField] private Animator m_Animator;
		#endregion


		#region UNITY EVENTS
		void OnEnable()
		{
			stage = GetComponent<Stage>();
		}
		#endregion


		#region INTERFACE IMPLEMENTATION - IStageBehaviour
		public void StartBehavior()
		{
			StartCoroutine(StartBehaviourSequence());
		}

		public abstract IEnumerator StartBehaviourSequence();
		#endregion


		#region HELPER FUNCTIONS
		protected void RaiseStageProgressChanged(StageProgress progress)
		{
			onStageProgressChanged(progress);
		}
		#endregion
	}
}