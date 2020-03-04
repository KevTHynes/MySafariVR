using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationsAnimator : MonoBehaviour
{
    private Animator anim;

    private Camera viewPoint;

    public bool choseGrizzlyGarden, choseSharkReef, choseWolfMountain;

    void Awake()
    {
        anim = GetComponent<Animator>();

        viewPoint = Camera.main; 
    }
    // Start is called before the first frame update
    void Start()
    {
        choseGrizzlyGarden = false;
        choseSharkReef = false;
        choseWolfMountain = false;

        TurnOffButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // If the animator is enabled, lets look at the dashboard buttons
        if (anim.enabled)
        {
            LookAtButtons();
        }
    }

    // Method for turning on the buttons attached to the signs
    // These are used to control the choice of locations the player can travel to
    public void TurnOnButtons()
    {
        // Turn on the animator
        anim.enabled = true;
    }

    // Method for turning off the buttons
    public void TurnOffButtons()
    {
        // Turn on the animator
        anim.enabled = false;
    }

    public void LookAtButtons()
    {
        // Calling the raycaster reference we use to look at objects
        RaycastHit hit;
        Ray ray = new Ray(viewPoint.transform.position, viewPoint.transform.rotation * Vector3.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If look at left directional button, animate the left button green light
            if (hit.collider.tag == "BearLocation")
            {
                Debug.Log("Bear Button");
                GrizzlyGardensButton(true);
                SharkReefButton(false);
                WolfMountainButton(false);

                choseGrizzlyGarden = true;
            }
            // If look at right directional button, animate the right button green light
            else if (hit.collider.tag == "SharkLocation")
            {
                Debug.Log("Shark Button");
                GrizzlyGardensButton(false);
                SharkReefButton(true);
                WolfMountainButton(false);

                choseSharkReef = true;
            }
            // If look at right directional button, animate the right button green light
            else if (hit.collider.tag == "WolfLocation")
            {
                Debug.Log("Wolf Button");
                GrizzlyGardensButton(false);
                SharkReefButton(false);
                WolfMountainButton(true);

                choseWolfMountain = true;
            }
            // else, animate both buttons to flash
            else
            {
                GrizzlyGardensButton(false);
                SharkReefButton(false);
                WolfMountainButton(false);

                choseGrizzlyGarden = false;
                choseSharkReef = false;
                choseWolfMountain = false;
            }
        }
    }

    // Method for animating left button to be highlighted
    public void GrizzlyGardensButton(bool bear_Button)
    {
        anim.SetBool("IsBearLevel", bear_Button);
    }

    // Method for animating left button to be highlighted
    public void SharkReefButton(bool shark_Button)
    {
        anim.SetBool("IsSharkLevel", shark_Button);
    }

    // Method for animating left button to be highlighted
    public void WolfMountainButton(bool wolf_Button)
    {
        anim.SetBool("IsWolfLevel", wolf_Button);
    }
}
