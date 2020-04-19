using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Nudi.Fpsproject
{
    public class MainMenu : MonoBehaviour
    {


        public void Play()
        {
            SceneManager.LoadScene(1);
            // if not multiplayer
            // unity engine scene management + scenemanager loadscene
        }

        public void Option()
        {
            SceneManager.LoadScene("Option");
        }

        public void QuitGame()
        {
            Application.Quit();
        }


    }
}