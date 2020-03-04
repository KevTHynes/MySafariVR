using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardAnimator : MonoBehaviour
{
    private Animator anim;

    private GameObject raycaster;
    private Raycaster rayScript;

    public bool chose_Left, chose_Right;

    void Awake()
    {
        anim = GetComponent<Animator>();

        raycaster = GameObject.FindGameObjectWithTag("Raycaster");
        rayScript = raycaster.GetComponent<Raycaster>();

    }

    // Lets turn the dashboard buttons off for the moment until we need them
    void Start()
    {
        anim.enabled = false;

        chose_Left = false;
        chose_Right = false;
    }

    private void Update()
    {
        // If the animator is enabled, lets look at the dashboard buttons
        if (anim.enabled)
        {
            LookAtDash();
        }
    }

    // Method for turning on the buttons inside the dashboard in the cart
    // These are used to control the choice of directions the player is provided
    public void TurnOnDash()
    {
        // Turn on the animator
        anim.enabled = true;

        // Turn on the raycaster object
        rayScript.TurnOnRayCaster();
    }

    // Method for turning off the buttons inside the dashboard in the cart
    public void TurnOffDash()
    {
        // Turn on the animator
        anim.enabled = false;

        // Turn on the raycaster object
        rayScript.TurnOffRayCaster();
    }

    public void LookAtDash()
    {
        // Calling the raycaster object we use to look at objects
        RaycastHit hit;

        // Making sure we are only checking the raycast from the Z axis looking forward
        Vector3 fwd = raycaster.transform.TransformDirection(Vector3.forward);

        // Lets draw a line of where we want to know what direction/distance the raycast will go
        Debug.DrawRay(raycaster.transform.position, fwd * 25, Color.green);


        if (Physics.Raycast(raycaster.transform.position, fwd, out hit, 25))
        {
            // If look at left directional button, animate the left button green light
            if (hit.collider.tag == "Left Button")
            {
                LeftButton(true);
                RightButton(false);

                chose_Left = true;
            }
            // If look at right directional button, animate the right button green light
            else if (hit.collider.tag == "Right Button")
            {
                LeftButton(false);
                RightButton(true);

                chose_Right = true;
            }
            // else, animate both buttons to flash
            else
            {
                LeftButton(false);
                RightButton(false);

                chose_Left = false;
                chose_Right = false;
            }
        }
    }



    // Method for animating left button to be highlighted
    public void LeftButton(bool highlight_Left)
    {
        anim.SetBool("Left", highlight_Left);

        

        
    }

    // Method for animating right button to be highlighted
    public void RightButton(bool highlight_Right)
    {
        anim.SetBool("Right", highlight_Right);

        
    }
}
