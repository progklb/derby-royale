using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Cameras
{
	/// <summary>
	/// Simple follow camera to follow a specified target.
	/// </summary>
	[RequireComponent(typeof(Camera))]
	public class FollowCamera : MonoBehaviour
	{
		#region PROPERTIES
		public new Camera camera { get => m_Camera ?? (m_Camera = GetComponent<Camera>()); }

		public Transform followTarget { get => m_Target; set => m_Target = value; }
		#endregion


		#region VARIABLES
		private Camera m_Camera;

		private Vector3 m_Velocity;
		#endregion


		#region EDITOR FIELDS
		[SerializeField] private Transform m_Target;
		[Space]
		[SerializeField] private Vector3 m_PositionOffset;
		[SerializeField] private float m_LookAtYOffset;
		[Space]
		[SerializeField] private float m_PositionSmoothing = 0.15f;
		[SerializeField] private float m_LookAtSmoothing = 0.15f;
		#endregion


		#region UNITY EVENTS
		void Update()
		{
			if (followTarget != null)
			{
				FollowTarget();
			}
			else
			{
				Debug.LogError("This follow camera must be assigned to a target.");
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void FollowTarget()
		{
			// Use local-space offset for Z and X, but world-offset for Y so that the camera is always above.
			var targetPosition = followTarget.TransformPoint(m_PositionOffset);
			targetPosition.y = followTarget.position.y + m_PositionOffset.y;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_PositionSmoothing);

			var targetLookAt = Quaternion.LookRotation(m_Target.position + new Vector3(0f, m_LookAtYOffset, 0f) - transform.position, Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetLookAt, Time.deltaTime * m_LookAtSmoothing);
		}
		#endregion
	}
}