using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SharkState//Method to set states
{
    PATROL,
    CHASE,
    FEED,
    EAT
}// End Enumerator Method


public class SharkController : MonoBehaviour
{
    private AISeaAnimator anim;
    private SeaCreaturesHandler sea_Handler;

    private SharkState gws_State;

    private float current_Swim_Speed;

    private bool has_Waypoint = false;
    private readonly bool is_Turning;

    public bool is_Eating;

    private Collider collider_Self;

    //Variables for the current waypoint so we dont hit the same waypoint twice
    private Vector3 way_Point;
    private Vector3 last_Way_Point = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        // The Script attached to the Parent Gameobject in which the sea creature can identify and choose a waypoint to travel 
        sea_Handler = transform.parent.GetComponentInParent<SeaCreaturesHandler>();

        // The animator script that is attached to this creature
        anim = GetComponent<AISeaAnimator>();

    }// End Awake Method

    // Start is called before the first frame update
    void Start()
    {
        CheckHasCollider();
        gws_State = SharkState.PATROL;

        is_Eating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gws_State == SharkState.PATROL)
        {
            Patrol();
        }

        if (gws_State == SharkState.CHASE)
        {
            Chase();
        }

        if (gws_State == SharkState.FEED)
        {
            Feed();
        }

        if (gws_State == SharkState.EAT)
        {
            Eat();
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

            // Check to see if there is something blocking the path
            AvoidObstacle();

        }

        // Once the creature reaches the waypoint set has_Waypoint back to false
        if (transform.position == way_Point)
        {
            has_Waypoint = false;
        }

    }// End Patrol method

    private void Chase()
    {
        throw new NotImplementedException();
    }

    private void Feed()
    {
        throw new NotImplementedException();
    }

    private void Eat()
    {
        throw new NotImplementedException();
    }

    private bool FindWayPoint(float start = 1f, float end = 4f)
    {
        // Find a waypoint by choosing one at random
        way_Point = sea_Handler.RandomWaypoint();

        // Check to make sure the creature is not going to the same waypoint otherwise he will stick
        if (last_Way_Point == way_Point)
        {
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

    private void RotateCreature(Vector3 way_Point, float turn_Speed)
    {
        // Set a random speed for rotating to face a waypoint
        float current_Turn_Speed = turn_Speed * UnityEngine.Random.Range(1f, 5f);

        // Look at the waypoint the creature is travelling to
        Vector3 LookAt = way_Point - this.transform.position;

        // Return the current rotating speed to the creature
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), current_Turn_Speed * Time.deltaTime);

    }// End RotateCreature Method

    private void CheckHasCollider()
    {
        if (transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
        {
            collider_Self = this.transform.GetComponent<Collider>();

            //Debug.Log(collider_Self.name + " is the collider of " + this.transform.GetComponent<Collider>().name);
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

    public SharkState GWSharkstate { get; set; }

}
