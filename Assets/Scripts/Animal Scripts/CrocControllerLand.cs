using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CrocState//Method to set states
{
    REST,
    PATROL,
    CHASE,
    FEED,
    EAT
}// End Enumerator Method

public class CrocControllerLand : MonoBehaviour
{
    public CrocControllerLand croc;
    // Boolean to determine what type of Croc he is
    public bool is_Active_Croc, is_Lazy_Croc;

    // Declaration of the Croc's animator
    private AILandAnimator anim;
    // Declaration of the Croc's Navmesh agent controls
    private NavMeshAgent agent;

    private FoodList foodList;

    // Decalration of players coordinates
    private Transform player;

    // The collider attached to the Croc's mouth
    public GameObject eat_Point;
    private EatScript eat;

    // The layermask used to find food
    public LayerMask hunting;

    // The audio attached to this Croc
    private AnimalAudio croc_Audio;

    // Where we will store the food and nearest food location
    //public static List<Collider> foods = new List<Collider>();
    public static List<Collider> foods;
    private Collider nearest_Food;

    private float foodRadius = 50f;

    // Use of a boolean determines if there is food or not
    private bool has_Food;

    // Use of boolean determines if the player should be chased or not
    private bool is_Fed;

    public bool is_Eating;

    // Determining the Croc's state
    private CrocState croc_State;

    // Controllng the Croc's moving speed
    [SerializeField] private float walk_Speed = 1.5f;
    [SerializeField] private float run_Speed = 5f;

    [SerializeField] private float chase_Distance = 30f;
    private float no_Chase_Distance = 0f;
    private float current_Chase_Distance;

    // Determine when to stop before eating
    [SerializeField] private float eat_Distance = 4f;
    [SerializeField] private float move_After_Eat_Distance = 2f;

    [SerializeField] private float wait_Before_Eat = 2f;
    private float eat_Timer;

    // Time taken patrolling in a given radius
    [SerializeField] private float patrol_Radius_Min = 10f, patrol_Radius_Max = 20f;
    [SerializeField] private float patrol_For_This_Time = 10f;
    private float patrol_Timer;

    private void Awake()
    {
        if (is_Active_Croc || is_Lazy_Croc)
        {

            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<AILandAnimator>();

            player = GameObject.FindGameObjectWithTag("Player").transform;

            croc_Audio = GetComponentInChildren<AnimalAudio>();

            eat = eat_Point.GetComponent<EatScript>();

            foodList = this.gameObject.GetComponentInChildren<FoodList>();
        }

    }// End Awake Method

    // Start is called before the first frame update
    void Start()
    {
        if (is_Active_Croc)
        {
            //set default state to Patrol
            croc_State = CrocState.PATROL;

            //set time of patrolling as declared patrol time
            patrol_Timer = patrol_For_This_Time;

            
        }

        if (is_Lazy_Croc)
        {
            //set default state to Resting
            croc_State = CrocState.REST;
        }

        if (is_Lazy_Croc || is_Active_Croc)
        {
            // when the animal first gets to the food, eat right away
            eat_Timer = wait_Before_Eat;

            has_Food = false;

            is_Fed = false;

            is_Eating = false;

            if (foodList != null)
            {
                Debug.Log("The " + this.gameObject.name + " now has a component: "  + this.foodList.name);

                if (foodList.foods!=null)
                {
                    Debug.Log("The " + this.gameObject.name + " now has a list ");
                }
            }

            
        }

    }// End Start Method

    /*
     * Update checks what state either of the crocs are in:
     * 
     * If 'Lazy Croc':
     *                  - The croc is lazy and will not move around nor chase the player if spotted but will growl
     *                  - The croc will be on the look out for food and will only move if food is detected and eat
     *                  - Once the croc has finished eating, it will go back to being lazy :)
     *                  
     * If 'Active Croc':
     *                  - The croc will patrol the area in search of food and the player
     *                  - If the croc sees the player he will attack
     *                  - If the croc detects food he will move to the foods location and eat
     */

    void Update()
    {
       
        // If its the Lazy Croc
        if (is_Lazy_Croc)
        {
            if (croc_State == CrocState.REST)
            {
                Rest();
            }
        }

        // If its the Active Croc
        if (is_Active_Croc)
        {
            if (croc_State == CrocState.PATROL)
            {
                Patrol();
            }

            if (croc_State == CrocState.CHASE)
            {
                Chase();
            }
        }

        // If its the Lazy Croc or the Active Croc
        if (is_Lazy_Croc || is_Active_Croc)
        {

            // Be on the hunt for food
            if (!has_Food)
            {
                has_Food = GetFoodLocation();
            }

            if (croc_State == CrocState.FEED)
            {
                Feed();
            }

            if (croc_State == CrocState.EAT)
            {
                Eat();
            }
        }

    }// End Update Method

    /*
     * The following methods determine which STATE the animal is in.
     * Each state determines which animation will be played through the animals animator
     * 
     * Patrol - The animal patrols an area by moving to a random location in the given radius
     * 
     * Alert - The animal has spotted the player, plays the corresponding ALERT animation and is ready to attack
     * 
     * Chase - The animal is attacking and runs to the players location
     * 
     * Feed - The animal detects that there is food close by and will run to the nearest food's location
     * 
     * Eat - The animal stops at the nearest food's location and plays the eating animation.
     * 
    */

    private void Rest()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        // Check if there is food, if so, start FEED state
        if (foodList.foods.Count > 0)
        {
            croc_State = CrocState.FEED;
        }

        // If distance between Croc and Player is less/equal to chase distance, the Croc spots the Player
        if (Vector3.Distance(transform.position, player.position) <= chase_Distance)
        {
            Debug.Log("The Croc has spotted the player!");

            //We need to make sure the Croc is looking at the player before he growls at the player!
            LookAtPlayer(player);

            croc_Audio.Play_RoarSound();
        }
    }

    private void Patrol()
    {
        // Tell Croc that he can move
        agent.isStopped = false;
        agent.speed = walk_Speed;

        // Add to the patrol timer
        patrol_Timer += Time.deltaTime;

        // If the time of patrol is bigger than the given time, we use this to execute a new destination to patrol to
        // then we reset the patrol timer until we can execute this method again...
        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;
        }

        // Check if Croc is moving, if so turn on walk animation...
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);

        }

        // Check if there is food, if so, start FEED state
        if (foodList.foods.Count > 0)
        {
            //LookAtFood(nearest_Food);

            croc_State = CrocState.FEED;

            anim.Walk(false);
        }

        // Check the distance between the Croc and the Player
        // if distance is less than or equal to given distance, chase the player!
        if (Vector3.Distance(transform.position, player.position) <= chase_Distance)
        {
            croc_Audio.Play_RoarSound();

            //We need to make sure the Croc is looking at the player before he Howls at the player!
            LookAtPlayer(player);

            //Enter Chase State
            croc_State = CrocState.CHASE;

            //He is no longer walking so turn the animation off
            anim.Walk(false);
        }

    }// End Patrol Method

    private void Chase()
    {
        // Tell the Croc that he can move
        agent.isStopped = false;
        agent.speed = run_Speed;

        // Check if Croc is moving, if so turn on run animation
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Run(true);
        }
        else
        {
            anim.Run(false);
        }

        // Run to players location
        agent.SetDestination(player.position);

        // Check if there is food, if so, start FEED state
        if (foodList.foods.Count > 0)
        {
            //LookAtFood(nearest_Food);

            croc_State = CrocState.FEED;
        }

    }// End Chase Method

    private void Feed()
    {
        // tell the Croc that he can move
        agent.isStopped = false;
        agent.speed = run_Speed;

        // Check if Croc is moving, if so turn on run animation...
        // if not, Idle animation is default and we turn off Run animation
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Run(true);
        }
        else
        {
            anim.Run(false);
        }

        agent.SetDestination(nearest_Food.transform.position);

        // Check that the Croc does not move right on top of food so that he can take a bite...
        // so if the distance between Bear and Food is less than eating distance then eat the food
        // this is how the Croc knows the exact time to stop so he can start the EAT animation
        if (Vector3.Distance(transform.position, nearest_Food.transform.position) <= eat_Distance)
        {
            // stop the animations
            anim.Run(false);

            // Make sure the animal is facing the food!
            LookAtFood(nearest_Food);

            // Enter the eating animation
            croc_State = CrocState.EAT;
        }
    }// End Feed Method

    private void Eat()
    {
        // Tell the Croc to stop moving because he is eating
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        eat_Timer += Time.deltaTime;

        // We need this so the Croc can eat more fluently, having pauses in bewtween animation cycles
        // otherwise the animation will run over and over
        if (eat_Timer > wait_Before_Eat)
        {
            anim.Eat();

            eat_Timer = 0f;

            is_Eating = true;

        }

        // Check if the food has been eaten, if so, call the list and remove any eaten food
        if (!nearest_Food.gameObject.activeInHierarchy)
        {
            RemoveEaten();

            // A condition used to make the animal not notice the player anymore
            is_Fed = true;
        }

        // Croc_Is_Fed is set to true as we will need this later for the players achievements
        if (is_Fed)
        {
            Croc_Is_Fed = true;

            chase_Distance = no_Chase_Distance;
        }

        // Check if the food is gone, if so, go back to PATROL state
        if (foodList.foods.Count == 0)
        {
            is_Eating = false;

            if (is_Active_Croc)
            {
                croc_State = CrocState.PATROL;

                // reset the patrol timer so that the function
                // can calculate the new patrol destination right away
                patrol_Timer = patrol_For_This_Time;
            }

            if (is_Lazy_Croc)
            {
                croc_State = CrocState.REST;

            }
        }

    }// End Eat Method

    /*
     * The following methods are helper method for each STATE that the animal may be in
     * 
     * SetNewRandomDestination() - Calculates a direction and location to travel based off a declared radius
     *                             and uses the NavMesh tools to determine walkable and non walkable areas
     * 
     * GetFoodLocation() - Detects food in a given radius. Calculates the nearest piece of food to the animal and stores
     *                     that foods location in a list 
     * 
     * LookAtPlayer() - A helper method that gives the animal a mre fluent way of looking at the player. This is needed
     *                  to make sure the animal is looking at the player before playing the ALERT animation
     *                
     * LookAtFood() - Similar approach the the LookAtPlayer. The only difference is that the animal is looking at the food so
     *                when the EAT animation is playing, it should be facing the food for the animals mouth collider to 
     *                interact with the piece of food.
     * 
     * RemoveEaten() - Method used to loop through any eaten food and remove them from the list
     * 
     * Turn_On_EatPoint() / Turn_Off_EatPoint() - Activates the Gameobject in the animals mouth which holds the collider
     *                                            used to interact with the piece of food. It is triggered by an animation
     *                                            event located in the EAT animation
     */

    private void SetNewRandomDestination()
    {
        // Determine the radius on how far the Croc will walk
        float rand_Radius = UnityEngine.Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = UnityEngine.Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        agent.SetDestination(navHit.position);

    }// End SetNewRandomDestination Method

    private bool GetFoodLocation()
    {
        // Only look for look for objects of food 'Hunting Layermask'
        Collider[] foodOpts = Physics.OverlapSphere(this.transform.position, foodRadius, hunting);

        float nearDist = float.MaxValue;

        // Loop through the food in the given radius
        foreach (Collider food in foodOpts)
        {
            // Calculate the nearest piece of food
            float thisDist = (transform.position - food.transform.position).sqrMagnitude;

            if (thisDist < nearDist)
            {
                // We need to make sure the piece of thrown food has stopped moving...
                // if so, add the nearest piece of food to the list
                if (food.attachedRigidbody.IsSleeping())
                {
                    nearDist = thisDist;

                    nearest_Food = food;

                    foodList.foods.Add(nearest_Food);

                    Debug.Log("The food added to the " + this.gameObject.name + "'s list: " + nearest_Food.name);

                    Debug.Log("The current number of foods in the " + this.gameObject.name + "'s list is: " + foodList.foods.Count);

                    return true;
                }
            }
        }

        return false;

    }// End GetFoodLocation Method

    private void LookAtPlayer(Transform player)
    {
        Vector3 directionToLook = (player.position - transform.position).normalized;

        float turnSpeed = 1.5f;

        Quaternion lookRotation = Quaternion.LookRotation(directionToLook);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

    }// End LookAtPlayer Method

    private void LookAtFood(Collider nearest_Food)
    {
        Vector3 directionToLook = (nearest_Food.transform.position - transform.position).normalized;

        float turnSpeed = agent.angularSpeed;

        Quaternion lookRotation = Quaternion.LookRotation(directionToLook);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

    }// End LookAtFood Method

    private void RemoveEaten()
    {
        for (int i = 0; i < foodList.foods.Count; i++)
        {
            // Make sure the foods are inactive, if so, remove from the list 
            if (!foodList.foods[i].gameObject.activeInHierarchy)
            {
                foodList.foods.RemoveAt(i);
                Debug.Log("Food at position " + i + " Was removed from the " + this.gameObject.name + "'s list!");
                i--;
            }
            Debug.Log("The current foods in the list after deletion is: " + foodList.foods.Count);
        }
    }// End RemoveEaten Method

    //Method for activating collider in Croc's mouth, this is needed for his mouth to inflict damage to the food
    void Turn_On_EatPoint()
    {
        eat_Point.SetActive(true);

    }// End Turn_On_EatPoint Method

    //Method for deactivating collider in Croc's mouth, this happens at end of each eating animation cycle
    void Turn_Off_EatPoint()
    {
        if (eat_Point.activeInHierarchy)
        {
            eat_Point.SetActive(false);
        }

    }// End Turn_Off_EatPoint Method


    //Public because we need to access this method outside of the class
    public CrocState CrocState { get; private set; }

    //Public because we need to access this method outside of the class
    public bool Croc_Is_Fed { get; private set; }
}
