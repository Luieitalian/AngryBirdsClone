using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace berkepite
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField]
        private float sceneLoadDelay;
        private SceneLoader sceneLoader;

        void Awake()
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }

        void Start()
        {
            StartCoroutine(StartLoading(sceneLoadDelay));
        }

        IEnumerator StartLoading(float time)
        {
            yield return new WaitForSeconds(time);
            // Load Main Menu
            sceneLoader.LoadScene(1);
        }

    }
}
