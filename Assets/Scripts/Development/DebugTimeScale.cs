using UnityEngine;

using UInput = UnityEngine.Input;

namespace DerbyRoyale.Development
{
	public class DebugTimeScale : MonoBehaviour
	{
		#region UNITY EVENTS
		void Update()
		{
			if (UInput.GetKey(KeyCode.LeftShift))
			{
				if (UInput.GetKey(KeyCode.Alpha1))
				{
					Time.timeScale = 1f;
				}
				else if (UInput.GetKey(KeyCode.Alpha2)) 
				{
					Time.timeScale = 3f;
				}
				else if (UInput.GetKey(KeyCode.Alpha3))
				{
					Time.timeScale = 5f;
				}
			}
		}
		#endregion
	}
}