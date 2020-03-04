using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.AI;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    // make a reference to the player as it is the players components we will need here...
    private GameObject player;

    // The reference to the players cart
    private CinemachineDollyCart cart;

    public bool isBear, isWolf;

    private BearController bear;
    private AlphaWolfController wolf;

    private NavMeshAgent nav;

    private Animator animator;

    // Dialogue audio, attach to this script
    public GameObject dManager;
    private DialogueManager dialogue;

    // Boolean where we will restart the game
    public bool restart;

    private void Awake()
    {
        // Player components
        player = GameObject.FindGameObjectWithTag("Player");
        cart = player.GetComponentInParent<CinemachineDollyCart>();

        // Declare the dialogue audio in the scene
        dialogue = dManager.GetComponent<DialogueManager>();

        if (isBear)
        {
            // if the animal is a bear, get the necessary components
            bear = GetComponent<BearController>();
            nav = this.GetComponent<NavMeshAgent>();
            animator = this.GetComponent<Animator>();

        }

        if (isWolf)
        {
            // if the animal is a wolf, get the necessary components
            wolf = GetComponent<AlphaWolfController>();
            nav = this.GetComponent<NavMeshAgent>();
            animator = this.GetComponent<Animator>();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
    }

    // On collision to check if this animal has collided with the player...
    private void OnTriggerEnter(Collider col)
    {
        // If the animal collides with the player, lets save him/her before being eaten...
        if (col.gameObject.Equals(player))
        {
            // Stop moving the player
            cart.m_Speed = 0;

            // now lets turn off the components so the animal stops
            if (isBear)
            {
                // bring movement to 0
                nav.velocity = Vector3.zero;

                nav.isStopped = true;

                // turn off the controller
                bear.enabled = false;

                animator.enabled = false;

                // start a coroutine that will load the desired scene.
                StartCoroutine(RestartScene());

            }

            if (isWolf)
            {
                // We need to stop the puppies too...
                GameObject[] pups = GameObject.FindGameObjectsWithTag("Pup");

                // Loop through the puppies in the scene and turn off their components
                for(int i = 0; i < pups.Length; i++)
                {
                    pups[i].GetComponent<WolfPupController>().enabled = false;

                    pups[i].GetComponent<Animator>().enabled = false;

                    pups[i].GetComponent<NavMeshAgent>().velocity = Vector3.zero;


                }

                // bring movement to 0
                nav.velocity = Vector3.zero;

                nav.isStopped = true;

                // turn off the controller
                wolf.enabled = false;

                animator.enabled = false;
                // start a coroutine that will load the desired scene.
                StartCoroutine(RestartScene());

            }

        }
    }

    // The coroutine runs to restart the scene.
    IEnumerator RestartScene()
    {
        // Play the collision audio
        dialogue.PlayDialogue("Collision");

        // Wait until the voice dialogue has finished
        yield return new WaitForSeconds(7f);

        // Start an asynchronous operation to reload the active scene..
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // While the asynchronous operation to load the scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("Current loading progress.. " + progress);
            yield return null;

        }

    }
}
