using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Smoove.Utilities;

namespace Smoove.Core
{
    /// <summary>
    /// Handles asynchronous scene loading with optional loading feedback.
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] private float _minimumLoadTime = 0.5f;

        public bool IsLoading { get; private set; }

        /// <summary>
        /// Loads a scene by name asynchronously.
        /// </summary>
        public void LoadScene(string sceneName)
        {
            if (IsLoading) return;
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        /// <summary>
        /// Loads a scene by build index asynchronously.
        /// </summary>
        public void LoadScene(int buildIndex)
        {
            if (IsLoading) return;
            StartCoroutine(LoadSceneRoutine(buildIndex));
        }

        public void ReloadCurrentScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            IsLoading = true;
            GameManager.Instance.SetState(GameState.Loading);

            float elapsed = 0f;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                elapsed += Time.deltaTime;

                if (operation.progress >= 0.9f && elapsed >= _minimumLoadTime)
                    operation.allowSceneActivation = true;

                yield return null;
            }

            IsLoading = false;
        }

        private IEnumerator LoadSceneRoutine(int buildIndex)
        {
            IsLoading = true;
            GameManager.Instance.SetState(GameState.Loading);

            float elapsed = 0f;
            AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                elapsed += Time.deltaTime;

                if (operation.progress >= 0.9f && elapsed >= _minimumLoadTime)
                    operation.allowSceneActivation = true;

                yield return null;
            }

            IsLoading = false;
        }
    }
}
