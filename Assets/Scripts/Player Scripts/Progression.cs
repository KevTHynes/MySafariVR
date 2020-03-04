using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    private Path path;

    [SerializeField]
    private bool progression, pathDecider = false;

    private bool inProgressionArea, inPathDeciderArea = false;

    private void Awake()
    {
        path = GetComponentInParent<Path>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If we enter the area infront of achievements board
        if (inProgressionArea)
        {
            // if neither of the croc achievements have been met...
            if (Achievements.feed_Croc == false || Achievements.croc_Photo == false)
            {
                //Debug.Log("You failed to complete all achievements!");

                // This is where we left click and restart the scenario...
            }

            // If all achievements have been met...
            if (Achievements.feed_Croc == true && Achievements.croc_Photo == true)
            {
                //Debug.Log("You compelted all achievements!");

                // This is where we left click and continue to path decider...
            }

            // Press left click to continue...
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Left Click was pressed...!");
                path.Continue();
            }
        }

        //If we enter the are where we choose path to go
        if (inPathDeciderArea)
        {
            // turn on the dashboard so we can choose the path to go
            path.triggerEntered = true;
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        // If the player enters the area
        if (col.gameObject.tag == "Player")
        {
            //If the area is infront of achievements board
            if (progression)
            {
                Debug.Log("Entered the progression area!");

                // We are now in the progression area
                inProgressionArea = true;

                if (Achievements.feed_Croc == false)
                {
                    Debug.Log("You did not feed the croc");
                }

                if (Achievements.croc_Photo == false)
                {
                    Debug.Log("You did not photo the croc eating!");
                }
            }

            //If the area is infront of paths to choose
            if (pathDecider)
            {
                Debug.Log("Entered the path decider area!");
                // We are now in the area to choose a path
                inPathDeciderArea = true;
                
            }       
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (progression)
        {
            // Left the progression area infront of the achievements board
            inProgressionArea = false;

            Debug.Log("Left the progression area!!!");
        }

        if (pathDecider)
        {
            // Left the path decider area
            inPathDeciderArea = false;

            path.triggerEntered = false;

            Debug.Log("Left the path decider area!!!");
        }
    }


}
