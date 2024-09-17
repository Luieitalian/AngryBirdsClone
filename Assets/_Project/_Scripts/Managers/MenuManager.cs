using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private SceneLoader sceneLoader;

        void Awake()
        {
        }

        public void OnPlayPressed()
        {
            sceneLoader.LoadScene(2);
            print("Play Button Pressed!");
        }

        public void OnExitPressed()
        {
            print("Exit Button Pressed!");
            Application.Quit();
        }
    }
}
