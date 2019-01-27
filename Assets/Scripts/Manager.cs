using UnityEngine;

namespace DerbyRoyale
{
	public class Manager<T> : MonoBehaviour where T : Manager<T>
	{
		#region PROPERTIES
		public static T instance { get; private set; }
		#endregion


		#region UNITY EVENTS
		protected virtual void Awake()
		{
			if (instance == null)
			{
				instance = (T)this;
			}
			else
			{
				Debug.LogError($"An instance of manager {instance.GetType().Name} already exists in scene.");
			}
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}
		#endregion
	}
}