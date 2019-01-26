using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using UnitySceneManagement = UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DerbyRoyale
{
	public class SceneManager : MonoBehaviour
	{
		#region TYPES
		private enum SceneType
		{
			MainMenu = 0,
			GameScene = 1
		}
		#endregion


	    #region EDITOR FIELDS
		[SerializeField] private string m_MainMenuScene; 
		[SerializeField] private string m_GameScene; 
		#endregion


		#region PUBLIC API
		public void LoadScene(int sceneType)
		{
			StartCoroutine(LoadSceneSequence((SceneType)sceneType));
		}

		public void UnloadScene(int sceneType)
		{
			StartCoroutine(UnloadSceneSequence((SceneType)sceneType));
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator LoadSceneSequence(SceneType type)
		{
			yield return UnitySceneManager.LoadSceneAsync(GetSceneName(type), UnitySceneManagement.LoadSceneMode.Additive);
		}

		IEnumerator UnloadSceneSequence(SceneType type)
		{
			yield return UnitySceneManager.UnloadSceneAsync(GetSceneName(type));
		}
		
		string GetSceneName(SceneType type)
		{
			switch (type)
			{
				case SceneType.MainMenu:
					return m_MainMenuScene;
				case SceneType.GameScene:
					return m_GameScene;
				default:
					return null;
			}
		}
		#endregion
	}
}