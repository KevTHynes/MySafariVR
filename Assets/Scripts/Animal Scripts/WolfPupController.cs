using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum WolfPupState//Method to set states
{
    REST,
    PATROL,
    CHASE
}

public class WolfPupController : MonoBehaviour
{
    // Declaration of the Alpha's postition so the Pup can travel to when directed
    private Transform alpha_Wolf;

    // Declaration of the Wolf's animator
    private AILandAnimator anim;
    // Declaration of the Wolf's Navmesh agent controls
    private NavMeshAgent agent;

    // The audio attached to this Wolf
    private AnimalAudio wolf_Audio;

    // Determining the Wolf's state
    private WolfPupState agentState;

    // Controllng the Wolf's moving speed
    [SerializeField] private float walk_Speed = 3f;
    [SerializeField] private float run_Speed = 6f;

    // Set this as the distance the Alpha will be away from the Pup before running after him
    [SerializeField] private float alpha_Distance = 15f;

    // Determine when to stop when close enough to the Alpha
    [SerializeField] private float safe_Distance = 5f;

    // time taken patrolling in a certain radius
    [SerializeField] private float patrol_Radius_Min = 5f, patrol_Radius_Max = 10f;
    [SerializeField] private float patrol_For_This_Time = 5f;
    private float patrol_Timer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<AILandAnimator>();

        alpha_Wolf = GameObject.FindGameObjectWithTag("AlphaWolf").transform;

        wolf_Audio = GetComponentInChildren<AnimalAudio>();

    }

    // Start is called before the first frame update
    void Start()
    {
        agentState = WolfPupState.REST;

    }

    // Update is called once per frame
    void Update()
    {
        if (agentState == WolfPupState.REST)
        {
            Rest();
        }

        if (agentState == WolfPupState.PATROL)
        {
            Patrol();
        }

        if (agentState == WolfPupState.CHASE)
        {
            Chase();
        }
    }

   

    private void Rest()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        if (Vector3.Distance(transform.position, alpha_Wolf.position) >= alpha_Distance)
        {
            Debug.Log("The Pup is too far away from the Alpha Wolf");

            agentState = WolfPupState.CHASE;

            wolf_Audio.Play_RoarSound();
        }
    }


    private void Patrol()
    {
        // Tell Pup that he can move
        agent.isStopped = false;
        agent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        // If the time of patrol is bigger than the given time, we use this to execute a new destination to patrol to
        // then we reset the patrol timer until we can execute this method again...
        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;
        }

        // Check if Pup is moving, if so turn on walk animation...
        // if not, 'Idle animation' is default and we turn off walk animation
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }

        // Check the distance between the pup and the Alpha's postion, if its greater or equal...
        // run after the alpha.
        if (Vector3.Distance(transform.position, alpha_Wolf.position) >= alpha_Distance)
        {
            Debug.Log("The Pup is too far away from the Alpha Wolf");

            anim.Walk(false);

            agentState = WolfPupState.CHASE;

            wolf_Audio.Play_RoarSound();

        }
    }

   

    private void Chase()
    {
        // Tell the Pup that he can move
        agent.isStopped = false;
        agent.speed = run_Speed;

        // Check if Pup is moving, if so turn on walk animation...
        // if not, 'Idle animation' is default and we turn off walk animation
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Run(true);
        }
        else
        {
            anim.Run(false);
        }

        // Run to Alpha's location
        agent.SetDestination(alpha_Wolf.position);

        // Once the pup has reached the 'safe distance' which is in close proximity of the Alpha, go back to Resting state
        if (Vector3.Distance(transform.position, alpha_Wolf.position) <= safe_Distance)
        {
            // stop the animations
            anim.Run(false);

            // Enter the Idle animation
            agentState = WolfPupState.REST;
        }
    }

    /*
     *  Method where we check if the pup has been idle for too long, if so...
     *  have him walk for a little bit until he is too far away from the Alpha
     *  **This is controlled by an animataion event in the Animator**
    */
    private void IsBored(string status)
    {
        if (status.Equals("LetsWalk"))
        {
            Debug.Log("The Pup got bored");

            agentState = WolfPupState.PATROL;
        }
    }

    private void SetNewRandomDestination()
    {
        // Determine the radius on how far the Pup will walk
        float rand_Radius = UnityEngine.Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        //A random position is calculated within the given radius
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        // The calculated position is checked against layers in the scene such as areas it should not go aka off the map, blockers etc 
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        // Tell the agent to travel to the given position
        agent.SetDestination(navHit.position);
    }
}
