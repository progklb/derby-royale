using UnityEngine;

using DerbyRoyale.Vehicles;

namespace DerbyRoyale.Utilities
{
	/// <summary>
	/// Uses collision callbacks to destroy any objects that come into contact with this object.
	/// </summary>
	public class DestroyOnContact : MonoBehaviour
	{
		#region UNITY EVENTS
		void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.tag == Tags.PLAYER_TAG)
			{
				col.gameObject.GetComponentInChildren<Vehicle>().TrashCar();
			}
			else
			{
				DestroyObject(col.gameObject);
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void DestroyObject(GameObject obj)
		{
			Destroy(obj);
		}
		#endregion
	}
}