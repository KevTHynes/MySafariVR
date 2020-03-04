using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SeaCreatureState//Method to set states
{
    PATROL,
    FLEE
}// End Enumerator Method

public class SeaCreatureController : MonoBehaviour
{
    private AISeaAnimator anim;
    private SeaCreaturesHandler sea_Handler;

    private SeaCreatureState creature_State;

    // Decalration of  coordinates
    private Transform gw_Shark;
    private Transform meg_Shark;

    private float gw_Shark_Distance = 5f;

    // Swim speed
    [Range(1f, 2f)]private float swim_Speed;
    [Range(3f, 4f)] private float fast_Swim_Speed;
    private float current_Swim_Speed;

    private bool has_Waypoint = false;
    private readonly bool is_Turning;

    private Collider collider_Self;

    //Variables for the current waypoint so we dont hit the same waypoint twice
    private Vector3 way_Point;
    private Vector3 last_Way_Point = new Vector3(0f, 0f, 0f);

    private Vector3 destroy_Point;


    private void Awake()
    {             
        // The Script attached to the Parent Gameobject in which the sea creature can identify and choose a waypoint to travel 
        sea_Handler = transform.parent.GetComponentInParent<SeaCreaturesHandler>();


    }// End Awake Method 


    void Start()
    {
        // The transform coordinates for the great white shark and the Megaladon
        gw_Shark = GameObject.FindGameObjectWithTag("GreatWhite").transform;
        meg_Shark = GameObject.FindGameObjectWithTag("Megaladon").transform;

        // The animator script that is attached to this creature
        anim = GetComponent<AISeaAnimator>();       

        // Make sure the creature has a collider
        CheckHasCollider();

        creature_State = SeaCreatureState.PATROL;

    }// End Start Method

    void Update()
    {
        if (creature_State == SeaCreatureState.PATROL)
        {
            Patrol();
        }

        if (creature_State == SeaCreatureState.FLEE)
        {
            Flee();
        }

    }// End Update Method

    
    private void Patrol()
    {
        // Turn on the swimming animation
        anim.Swim(true);

        // Be aware for when the Great white shark or Megaladon is lurking near
        // If creature is near, Swim to safety
        if (Vector3.Distance(transform.position, gw_Shark.position) <= gw_Shark_Distance)
        {
            Debug.Log("The Creature " + this.name + " has spotted the Shark!");

            destroy_Point = sea_Handler.RandomDestroypoint();

            creature_State = SeaCreatureState.FLEE;

            anim.Swim(false);

        }
        else if (meg_Shark != null)
        {
            if (Vector3.Distance(transform.position, meg_Shark.position) <= gw_Shark_Distance)
            {
                Debug.Log("The Creature " + this.name + " has spotted the Megaladon!");

                destroy_Point = sea_Handler.RandomDestroypoint();

                creature_State = SeaCreatureState.FLEE;

                anim.Swim(false);

            }
        }
        
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

            // Check to see if there is something blocking the path
            AvoidObstacle();

        }

        // Once the creature reaches the waypoint set has_Waypoint back to false
        if (transform.position == way_Point)
        {
            has_Waypoint = false;
        }

    }// End Patrol method

    private void Flee()
    {       
        float flee_Speed = 5f;

        // Rotate and move to safety
        RotateFleeingCreature(destroy_Point, flee_Speed);

        transform.position = Vector3.MoveTowards(transform.position, destroy_Point, flee_Speed * Time.deltaTime);

        // Set the animation speed to travel speed
        anim.SwimSpeed(flee_Speed);

        if (transform.position == destroy_Point)
        {
            Destroy();
        }

    }// End Flee Method

    

    private bool FindWayPoint(float start = 1f, float end = 2.5f)
    {
        // Find a waypoint by choosing one at random
        way_Point = sea_Handler.RandomWaypoint();

        //Debug.Log("The fishy chose the " + way_Point.ToString() + " To go to.");

        // Check to make sure the creature is not going to the same waypoint otherwise he will stick
        if (last_Way_Point == way_Point)
        {
            //Debug.Log("The fishy last waypoint was " + last_Way_Point.ToString() + " .");

            // If it is the same, we find a new waypoint
            way_Point = sea_Handler.RandomWaypoint();

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

    private void CheckHasCollider()
    {
        if (transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
        {
            collider_Self = this.transform.GetComponent<Collider>();

            //Debug.Log(collider_Self.name + " is the collider of " + this.transform.GetComponent<Collider>().name);
        }
        else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
        {
            collider_Self = transform.GetComponentInChildren<Collider>();

            //Debug.Log(collider_Self.name + " is the collider of " + this.transform.GetComponentInChildren<Collider>().name);
        }

    }// End CheckHasCollider Method

    private void AvoidObstacle()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {
            // Only if the Sea Creature detects a Waypoint or itself then ignore and continue on the path
            if (hit.collider == collider_Self || hit.collider.tag == "Waypoint")
            {
                return;
            }

            // Else choose a time at random to change direction
            int randomNum = UnityEngine.Random.Range(1, 100);
            if (randomNum < 40)
                has_Waypoint = false;
        }

    }// End AvoidObstacle Method

    private void RotateCreature(Vector3 way_Point, float turn_Speed)
    {
        // Set a random speed for rotating to face a waypoint
        float current_Turn_Speed = turn_Speed * UnityEngine.Random.Range(1f, 5f);

        // Look at the waypoint the creature is travelling to
        Vector3 LookAt = way_Point - this.transform.position;

        // Return the current rotating speed to the creature
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), current_Turn_Speed * Time.deltaTime);

    }// End RotateCreature Method

    private void RotateFleeingCreature(Vector3 destroy_Point, float turn_Speed)
    {
        // Set a random speed for rotating to face a waypoint
        float current_Turn_Speed = turn_Speed * UnityEngine.Random.Range(1f, 5f);

        // Look at the waypoint the creature is travelling to
        Vector3 LookAt = destroy_Point - this.transform.position;

        // Return the current rotating speed to the creature
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), current_Turn_Speed * Time.deltaTime);

    }// End RotateFleeingCreature Method

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    public SeaCreatureState SCState { get; set; }
}
