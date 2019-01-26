using UnityEngine;

namespace DerbyRoyale
{
	public class DebugTimeScale : MonoBehaviour
	{
		#region UNITY EVENTS
		void Update()
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKey(KeyCode.Alpha1))
				{
					Time.timeScale = 1f;
				}
				else if (Input.GetKey(KeyCode.Alpha2))
				{
					Time.timeScale = 3f;
				}
				else if (Input.GetKey(KeyCode.Alpha3))
				{
					Time.timeScale = 5f;
				}
			}
		}
		#endregion
	}
}