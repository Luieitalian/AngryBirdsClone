using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class MenuManager : MonoBehaviour
    {

        void Awake()
        {

        }

        public void OnPlayPressed()
        {
            print("Play Button Pressed!");
        }

        public void OnExitPressed()
        {
            print("Exit Button Pressed!");
            Application.Quit();
        }
    }
}
