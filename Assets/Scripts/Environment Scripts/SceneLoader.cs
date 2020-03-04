using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;
    public string LoadingSceneName;

    // Update is called once per frame
    void Update()
    {

        // If the player has triggered load scene
        if (loadScene)
        {

            // start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene(LoadingSceneName));
            loadScene = false;
        }

    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene(string sceneName)
    {

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("Current loading progress.. " + progress);
            yield return null;

        }

    }

    void OnTriggerEnter(Collider col)
    {
        // If the player enters the area
        if (col.gameObject.tag == "Player")
        {
            loadScene = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        // If the player enters the area
        if (col.gameObject.tag == "Player")
        {
            loadScene = false;
        }
    }
}
