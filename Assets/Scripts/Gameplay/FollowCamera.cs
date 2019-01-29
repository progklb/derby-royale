using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Gameplay
{
	/// <summary>
	/// Simple follow camera to follow a specified target.
	/// </summary>
	public class FollowCamera : MonoBehaviour
	{
		#region PROPERTIES
		private Transform target { get => m_Target; }

		private DerbyCar player { get => GameManager.instance.playerInstance; }
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private Transform m_Target;
		[Space]
		[SerializeField] private Vector3 m_PositionOffset;
		[SerializeField] private float m_LookAtYOffset;
		[Space]
		[SerializeField] private float m_PositionSmoothing = 0.15f;
		[SerializeField] private float m_LookAtSmoothing = 0.15f;

		private Vector3 m_Velocity;
		#endregion


		#region UNITY EVENTS
		void Update()
		{
			if (target != null)
			{
				// Use local-space offset for Z and X, but world-offset for Y so that the camera is always above.
				var targetPosition = target.TransformPoint(m_PositionOffset);
				targetPosition.y = target.position.y + m_PositionOffset.y;
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_PositionSmoothing);

				var targetLookAt = Quaternion.LookRotation(m_Target.position + new Vector3(0f, m_LookAtYOffset, 0f) - transform.position, Vector3.up);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetLookAt, Time.deltaTime * m_LookAtSmoothing);
			}
			else if (player != null)
			{
				m_Target = player.transform;
			}
		}
		#endregion
	}
}