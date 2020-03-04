using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCreaturesHandler : MonoBehaviour
{
    /*
     * This class is a script attached to the Parent Gameobject which holds all Child Gameobjects including Sea
     * Creatures that have AI instructions and waypoints which these creatures use to travel
     * 
     * The following variables/methods which underline these key functions are:
     * 
     * 1. wayPoints - The list created which stores each waypoint
     * 
     * 2. GetWaypoints() - The method which will find the transform of all child waypoints by a specific tag
     *                     and add them to the list
     * 
     * 3. GetCreatures() - The method which will find the transform of all child Sea Creatures by a specific tag
     *                   - Then once they are found, attach the animator script and the AI script on run time 
     *                     which gives them the ability to animate and move depending on the instructions
     * 
     * 4. RandomWaypoint() - This method is used to look into the populated list of waypoints and return a single
     *                       random waypoint which each creature will access individually and recursively, identifying the path 
     *                       they will take to travel
     */

    private SeaCreatureController ai_controller;

    // The list the waypoints will be stored in
    public List<Transform> wayPoints = new List<Transform>();

    // The list the destroy points will be stored in
    public List<Transform> destroyPoints = new List<Transform>();

    // Method where we call our helper methods on run time
    private void Awake()
    {
        GetCreatures();

        GetWaypoints();

        RandomWaypoint();

        GetDestroypoints();

        RandomDestroypoint();

    }// End Awake Method 

   

    private void GetWaypoints()
    {
        // Get the children waypoint transforms from the parent fish tank
        Transform[] wp_List = this.transform.GetComponentsInChildren<Transform>();

        // Loop through the children and add each waypoint to the list
        for (int i = 0; i < wp_List.Length; i++)
        {
            if (wp_List[i].tag == "Waypoint")
            {
                wayPoints.Add(wp_List[i]);

                //Debug.Log("The waypoint " + wpList[i].name + " was added to the list");
            }
        }
    }// End GetWaypoints Method

    private void GetDestroypoints()
    {
        // Get the children waypoint transforms from the parent fish tank
        Transform[] dp_List = this.transform.GetComponentsInChildren<Transform>();

        // Loop through the children and add each waypoint to the list
        for (int i = 0; i < dp_List.Length; i++)
        {
            if (dp_List[i].tag == "Destroypoint")
            {
                destroyPoints.Add(dp_List[i]);

                //Debug.Log("The waypoint " + wpList[i].name + " was added to the list");
            }
        }
    }// End GetDestroypoints Method

    

    private void GetCreatures()
    {
        // Get the children Sea Creatures transforms from the parent fish tank
        Transform[] sea_Creatures = this.transform.GetComponentsInChildren<Transform>();

        // Loop through the sea creatures and attach the AI scripts to them
        for (int i = 0; i < sea_Creatures.Length; i++)
        {
            if (sea_Creatures[i].tag == "Fishy")
            {
                //Debug.Log("The sea creature of " + sea_Creatures[i].name + " exists");

                // Safeguard that we know who we are attatching the scripts to
                if (sea_Creatures[i].gameObject != null)
                {
                    sea_Creatures[i].gameObject.AddComponent<SeaCreatureController>();

                    sea_Creatures[i].gameObject.AddComponent<AISeaAnimator>();

                    // Check that each creature has the script successfully attached
                    SeaCreatureController controller = sea_Creatures[i].GetComponent<SeaCreatureController>();
                    AISeaAnimator animator = sea_Creatures[i].GetComponent<AISeaAnimator>();

                }
            }
        }

    }// End GetCreatures Method

    public Vector3 RandomWaypoint()
    {
        // Assign a random waypoint in the list 
        int randomWP = UnityEngine.Random.Range(0, (wayPoints.Count - 1));

        // Get the position of that random waypoint
        Vector3 randomWaypoint = wayPoints[randomWP].transform.position;

        // Return the coordinates of that random waypoint
        return randomWaypoint;

    }// End RandomWaypoint Method

    public Vector3 RandomDestroypoint()
    {
        // Assign a random destroy point in the list 
        int randomDP = UnityEngine.Random.Range(0, (destroyPoints.Count - 1));

        // Get the position of that random destroy point
        Vector3 randomDestroypoint = destroyPoints[randomDP].transform.position;

        // Return the coordinates of that random destroy point
        return randomDestroypoint;

    }// End RandomDestroypoint Method
}
