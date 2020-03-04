using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MegState//Method to set states
{
    PATROL,
    VANISH
}// End Enumerator Method

public class MegController : MonoBehaviour
{
    // Animator attached to this shark
    private AISeaAnimator anim;

    // States that the shark will be in
    private MegState meg_State;

    // Current speed of swimming which is assigned in various methods
    private float current_Swim_Speed;

    // To check if the there is a waypoint to travel
    private bool has_Waypoint = false;

    // Make sure the shark is turning and facing waypoints
    private bool is_Turning;

    // The transform containing the waypoints
    [SerializeField]
    private Transform megPoints;

    //Variables for the current waypoint so we dont hit the same waypoint twice
    private Vector3 way_Point;
    private Vector3 last_Way_Point = new Vector3(0f, 0f, 0f);

    // The list the waypoints will be stored in
    public List<Transform> wayPoints = new List<Transform>();

    private void Awake()
    {
        // The animator script that is attached to this creature
        anim = GetComponent<AISeaAnimator>();

        // Load the waypoints
        GetMegPoints();
    }
    // Start is called before the first frame update
    void Start()
    {
        meg_State = MegState.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if (meg_State == MegState.PATROL)
        {
            Patrol();
        }

        if (meg_State == MegState.VANISH)
        {
            Dissapear();
        }
    }

    private void Patrol()
    {
        // Turn on the swimming animation
        anim.Swim(true);

        
        // If we have no waypoint yet, find a waypoint
        if (!has_Waypoint)
        {
            has_Waypoint = FindWayPoint();
        }
        else
        {
            // Else, lets rotate and move towards the waypoint
            RotateCreature(way_Point, current_Swim_Speed);

            transform.position = Vector3.MoveTowards(transform.position, way_Point, current_Swim_Speed * Time.deltaTime);

        }

        // Once the creature reaches the waypoint set has_Waypoint back to false
        if (transform.position == way_Point)
        {
            //Debug.Log("Meg has reached position");
            has_Waypoint = false;
            
        }

        // If there are no more waypoints, change state
        if (wayPoints.Count == 0)
        {
            meg_State = MegState.VANISH;

        }
    }

    // Method for vanishing from the scene
    private void Dissapear()
    {
        anim.Swim(false);

        Destroy(this.gameObject);
    }


    private bool FindWayPoint(float start = 5f, float end = 10f)
    {
        // Find a waypoint
        way_Point = NextWayPoint();

        // Check to make sure the creature is not going to the same waypoint otherwise he will stick
        if (last_Way_Point == way_Point)
        {
            //Debug.Log("Meg is at the same waypoint");

            // if so, remove the current waypoint and find the next one
            RemoveWaypoint();

            way_Point = NextWayPoint();

            return false;

        }
        else
        {
            // Otherwise we now set the new waypoint as the last waypoint
            last_Way_Point = way_Point;

            // Find a random speed for the creature to move at
            current_Swim_Speed = UnityEngine.Random.Range(start, end);

            // Set the animation speed to travel speed
            anim.SwimSpeed(current_Swim_Speed);

            // Return true as we have our waypoint
            return true;

        }

    }// End FindWayPoint Method

    private void RotateCreature(Vector3 way_Point, float turn_Speed)
    {
        // Set a random speed for rotating to face a waypoint
        float current_Turn_Speed = turn_Speed * UnityEngine.Random.Range(1f, 5f);

        // Look at the waypoint the creature is travelling to
        Vector3 LookAt = way_Point - this.transform.position;

        // Return the current rotating speed to the creature
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), current_Turn_Speed * Time.deltaTime);

    }// End RotateCreature Method


    private void GetMegPoints()
    {
        // Get the waypoint transforms
        Transform[] mp_List = megPoints.GetComponentsInChildren<Transform>();

        // Loop through the children and add each waypoint to the list
        for (int i = 0; i < mp_List.Length; i++)
        {
            if (mp_List[i].tag == "Megpoint")
            {
                wayPoints.Add(mp_List[i]);

                //Debug.Log("The waypoint " + mp_List[i].name + " was added to the list");
            }
        }
    }// End GetMegPoints Method

    public Vector3 NextWayPoint()
    {
        //Debug.Log("Meg is calculating the nearest waypoint...");

        // For calculating the nearest distance
        float nearDist = float.MaxValue;

        // Loop through the waypoints in the list
        foreach (Transform wp in wayPoints)
        {
            // Calculate the nearest wp
            float thisDist = (transform.position - wp.transform.position).sqrMagnitude;

            // if the waypoint distance is nearest, set the nearast waypoint to travel
            if (thisDist < nearDist)
            {
                nearDist = thisDist;

                way_Point = wp.position;

                break;
            }            
        }
        
        return way_Point;
    }

    private void RemoveWaypoint()
    {
        // Loop through the list, make sure the current position is the waypoint we are deleting, then delete and reset
        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (wayPoints[i].position == last_Way_Point)
            {
                //Debug.Log("last_Way_Point @  " + last_Way_Point + " and waypoint: " + wayPoints[i].position + " is the same, lets remove and carry on");

                wayPoints.RemoveAt(i);
                i--;

                //Debug.Log("wp at position " + i + " Was removed");


                //Debug.Log("The current wp's in the list after deletion is: " + wayPoints.Count);
                break;
            }
        }
    }// End RemoveEaten Method
}
