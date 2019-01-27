using UnityEngine;

namespace DerbyRoyale.Gameplay
{
	public class FollowCamera : MonoBehaviour
	{
		#region EDITOR FIELDS
		[SerializeField] private Vector3 m_Offset;
		#endregion


		#region PROPERTIES
		private Transform playerTransform { get => GameManager.instance?.playerInstance?.transform; }
		#endregion


		#region UNITY EVENTS
		void Update()
		{
		}
		#endregion
	}
}