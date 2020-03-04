using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LocationChooser : MonoBehaviour
{
    private LocationsAnimator anim;

    private Collider coll;

    private GameObject player;

    private GazeCursor gaze;

    public bool isInZone;

    public bool loadScene;

    public bool restart;

    public string LoadingSceneName;

    private void Awake()
    {
        anim = GetComponentInParent<LocationsAnimator>();

        player = GameObject.FindGameObjectWithTag("Player");
        gaze = player.GetComponentInChildren<GazeCursor>();

        coll = this.transform.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isInZone = false;

        loadScene = false;

        restart = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If we are in the trigger zone lets turn on the button animations
        if (isInZone)
        {
            anim.TurnOnButtons();

            // While we are in the zone...

            // If gaze cursor is not on, turn it on
            if (!gaze.isOn)
            {
                gaze.isOn = true;
            }

            // if player chooses button to bear enclosure
            if (anim.choseGrizzlyGarden)
            {
                if (Input.GetButtonDown("Fire1"))
                {

                    loadScene = true;

                    LoadingSceneName = "GrizzlyGardens";

                }
            }
            // if player chooses button to shark enclosure
            else if (anim.choseSharkReef)
            {
                if (Input.GetButtonDown("Fire1"))
                {

                    restart = true;

                }
            }
            // if player chooses button to wolf enclosure
            else if (anim.choseWolfMountain)
            {
                if (Input.GetButtonDown("Fire1"))
                {

                    loadScene = true;

                    LoadingSceneName = "WolfMountain";

                }
            }
        }
        // Otherwise, no need to animate the buttons so we switch them off...
        else
        {
            anim.TurnOffButtons();
        }

        
        // if the player chooses to restart the level, load the current active scene
        if (restart)
        {
            // start a coroutine that will load the desired scene.
            StartCoroutine(RestartScene());

            // then reset this to false so we do not call it more than once
            restart = false;

            // Now lets turn off the collider so we can tell the area we have exited
            this.coll.enabled = false;
        }

        // If the player chooses a different level to travel, load the corresponding scene
        if (loadScene)
        {
            // start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene(LoadingSceneName));

            // then reset this to false so we do not call it more than once
            loadScene = false;

            // Now lets turn off the collider so we can tell the area we have exited
            this.coll.enabled = false;
        }

    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator RestartScene()
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("Current loading progress.. " + progress);
            yield return null;

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
            isInZone = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        // If the player exits the area
        if (col.gameObject.tag == "Player")
        {
            isInZone = false;
        }
    }
}
