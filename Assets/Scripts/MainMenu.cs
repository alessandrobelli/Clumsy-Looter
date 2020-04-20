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
            Invoke("launchGame",0.8f);

        }

        public void launchGame()
        {
            SceneManager.LoadScene(1);
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