using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace berkepite
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Slider loadingBar;
        [SerializeField]
        private Animator transitionAnimator;

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadingBar.value = progress;
                Debug.Log("loading...");

                if (operation.progress >= 0.9f)
                {
                    transitionAnimator.SetTrigger("Start");
                    break;
                }
                yield return null;
            }

            Debug.Log("Loaded!");
            StartCoroutine(SetAllowSceneActivation(operation));
        }

        private IEnumerator SetAllowSceneActivation(AsyncOperation operation)
        {
            yield return new WaitForSeconds(1f);
            operation.allowSceneActivation = true;
        }
    }
}
