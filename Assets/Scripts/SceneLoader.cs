using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(LoadAsyncOperation());


    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        gameLevel.allowSceneActivation = false;

        while (!gameLevel.isDone)
        {
            Debug.Log(Mathf.Clamp01(gameLevel.progress / 0.9f) * 100);
            // scene has loaded as much as possible, the last 10% can't be multi-threaded
            if (gameLevel.progress >= 0.9f)
            {
                // we finally show the scene
                gameLevel.allowSceneActivation = true;
            }

            yield return null;

        }


    }



    // Update is called once per frame
    void Update()
    {

    }
}
