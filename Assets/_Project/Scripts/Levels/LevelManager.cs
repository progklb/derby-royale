using UnityEngine;

namespace DerbyRoyale.Levels
{
	public class LevelManager : Manager<LevelManager>
	{
		#region PROPERTIES
		/// The controller of the current level (if active).
		public LevelController currentController { get; private set; }
		#endregion


		#region PUBLIC API - CONTROLLERS
		public void RegisterController(LevelController controller)
		{
			if (currentController == null)
			{
				currentController = controller;
			}
			else
			{
				Debug.LogError("The existing level controller must be unassigned before a new controller can be registered.");
			}
		}

		public void DeregisterController(LevelController controller)
		{
			if (currentController == controller)
			{
				currentController = null;
			}
			else
			{
				Debug.LogError("An unregistered controller is attempting to deregister. This is not allowed.");
			}
		}
		#endregion
	}
}