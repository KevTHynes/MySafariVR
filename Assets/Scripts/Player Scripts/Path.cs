using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Path : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rigid;

    private DashboardAnimator dash;
    private GazeCursor gaze;

    [SerializeField]
    private GameObject start_Path, progression_Path ,left_Path, right_Path;
    private CinemachineSmoothPath current_Path;

    private CinemachineDollyCart cart;


    //intiate trigger to choose a path
    public bool triggerEntered;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // this should change

        rigid = player.GetComponent<Rigidbody>();

        dash = player.GetComponentInChildren<DashboardAnimator>();

        gaze = player.GetComponentInChildren<GazeCursor>();

        cart = player.GetComponentInParent<CinemachineDollyCart>();

        current_Path = start_Path.GetComponent<CinemachineSmoothPath>();
    }

    // Start is called before the first frame update
    void Start()
    {
        triggerEntered = false;

        
        if (cart.m_Path == current_Path)
        {
            Debug.Log("We are on the first path!");
        }
        
    }

    private void Update()
    {
        // Check if the area we enter is the path decider       
        if (triggerEntered)
        {
            // Now make sure the player has stopped moving completely
            if (rigid.IsSleeping())
            {
                //Debug.Log("The player has stopped moving");

                // if so, initiate the dashboard animations
                dash.TurnOnDash();


                if (!gaze.isOn)
                {
                    gaze.isOn = true;
                }
            }

            // Left button animation
            // once this is highlighted and player left clicks
            // Take the left path
            if (dash.chose_Left)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    
                    GoLeft();

                }
            }
            // Right button animation
            // once this is highlighted and player left clicks
            // Take the right path
            else if (dash.chose_Right)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    
                    GoRight();
                   
                }
            }
        }
        else
        {
            dash.TurnOffDash();

        }
    }

    public void Continue()
    {
        // We continue on the second path
        current_Path = progression_Path.GetComponent<CinemachineSmoothPath>();

        // Assign the players path to the current path chosen
        cart.m_Path = current_Path;

        // Make sure we start at the beginning of the path
        cart.m_Position = 0;

        Debug.Log("You are now on " + progression_Path.name);
    }

    public void Restart()
    {
        // We restart the level as some achievements were not met...
    }

    public void GoLeft()
    {
        // We continue on the left path
        current_Path = left_Path.GetComponent<CinemachineSmoothPath>();

        // Assign the players path to the current path chosen
        cart.m_Path = current_Path;

        // Make sure we start at the beginning of the path
        cart.m_Position = 0;

        Debug.Log("You have chosen " + left_Path.name);
    }

    public void GoRight()
    {
        // We continue on the right path
        current_Path = right_Path.GetComponent<CinemachineSmoothPath>();

        // Assign the players path to the current path chosen
        cart.m_Path = current_Path;

        // Make sure we start at the beginning of the path
        cart.m_Position = 0;

        Debug.Log("You have chosen " + right_Path.name);
    }
}
