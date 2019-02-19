using UnityEngine;

using System.Collections;

using UnitySceneManagement = UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DerbyRoyale.Scenes
{
	public class SceneManager : Manager<SceneManager>
	{
	    #region EDITOR FIELDS
		[SerializeField] private string m_MainMenuScene; 
		[SerializeField] private string m_GameScene; 
		#endregion


		#region PUBLIC API
		public void LoadScene(SceneType sceneType)
		{
			StartCoroutine(LoadSceneSequence(sceneType));
		}

		public void UnloadScene(SceneType sceneType)
		{
			StartCoroutine(UnloadSceneSequence(sceneType));
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