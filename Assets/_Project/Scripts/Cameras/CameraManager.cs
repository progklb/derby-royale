using UnityEngine;

using System.Collections.Generic;

using DerbyRoyale.Players;

namespace DerbyRoyale.Cameras
{
	public class CameraManager : Manager<CameraManager>
	{
		#region EDITOR FIELDS
		[SerializeField] private FollowCamera m_FollowCameraPrefab;
		#endregion


		#region PROPERTIES
		/// Holds reference to all players that have been provided cameras.
		private Dictionary<Player, FollowCamera> activeCameras { get; set; } = new Dictionary<Player, FollowCamera>();
		/// A set of viewports for split-screen rendering.
		private Rect[][] viewports { get; set; }
		#endregion


		#region UNITY EVENTS
		protected override void Awake()
		{
			base.Awake();

			CreateViewports();
		}
		#endregion


		#region PUBLIC API
		public void Add(Player player)
		{
			if (!activeCameras.ContainsKey(player))
			{
				var cam = Instantiate(m_FollowCameraPrefab, transform).GetComponent<FollowCamera>();
				cam.followTarget = player.vehicleInstance.transform;

				activeCameras.Add(player, cam);

				RefreshCameras();
			}
		}

		public void Remove(Player player)
		{
			if (activeCameras.ContainsKey(player))
			{
				Destroy(activeCameras[player].gameObject);
				activeCameras.Remove(player);

				RefreshCameras();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		/// <summary>
		/// Creates a jagged array of viewports for split screen rendering.
		/// </summary>
		void CreateViewports()
		{
			var p1 = new Rect[] {
				new Rect(0.0f, 0.0f, 1.0f, 1.0f)
			};

			var p2 = new Rect[] {
				new Rect(0.0f, 0.0f, 1.0f, 0.5f),
				new Rect(0.0f, 0.5f, 1.0f, 0.5f)
			};

			var p3 = new Rect[] {
				new Rect(0.0f, 0.5f, 0.5f, 0.5f),
				new Rect(0.5f, 0.5f, 0.5f, 0.5f),
				new Rect(0.0f, 0.0f, 1.0f, 0.5f)
			};

			var p4 = new Rect[] {
				new Rect(0.0f, 0.5f, 0.5f, 0.5f),
				new Rect(0.5f, 0.5f, 0.5f, 0.5f),
				new Rect(0.0f, 0.0f, 0.5f, 0.5f),
				new Rect(0.5f, 0.0f, 0.5f, 0.5f),
			};

			viewports = new Rect[4][];
			viewports[0] = p1;
			viewports[1] = p2;
			viewports[2] = p3;
			viewports[3] = p4;
		}

		/// <summary>
		/// Refreshes cameras based on the number of cameras registered,
		/// and assigned viewports for split-screen rendering.
		/// </summary>
		void RefreshCameras()
		{
			if (activeCameras.Count > 0)
			{
				var views = viewports[activeCameras.Count - 1];
				int index = 0;

				foreach (var followCam in activeCameras.Values)
				{
					followCam.camera.rect = views[index++];
				}
			}
		}
		#endregion
	}
}