using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour
{
    public Camera viewPoint;
    public GameObject cursorPrefab;
    public float maxGazeDistance = 30;

    private GameObject cursorInstance;

    public bool isOn = false;

    // Use this for initialization
    void Start()
    {
        // if there is not cursor in the scene, create one...
        if (cursorInstance == null)
        {
            //Debug.Log("Cursor non existent");

            cursorInstance = Instantiate(cursorPrefab, transform);

            //Debug.Log("Cursor created");

        }
        // Else, destroy the instance
        else
        {
            Destroy(cursorInstance);

            //Debug.Log("Cursor destroyed");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the cursor is not checked as on, turn it off
        if (!isOn)
        {
            TurnOffCursor();
        }

        // if the cursor is checked as on, turn iy in
        if (isOn)
        {
            TurnOnCursor();
        }
        //UpdateCursor();
    }

    
    
    public void TurnOnCursor()
    {
        // if there is a cursor and we call this method, turn the cursor ON...
        if (cursorInstance != null)
        {
            cursorInstance.SetActive(true);

            //Debug.Log("Cursor on");

            UpdateCursor();
        }
    }

    public void TurnOffCursor()
    {
        // if there is a cursor and we call this method, turn the cursor OFF...
        if (cursorInstance != null)
        {
            cursorInstance.SetActive(false);

           // Debug.Log("Cursor off");
        }
    }
    
    /// <summary>
    /// Updates the cursor based on what the camera is pointed at.
    /// </summary>
    private void UpdateCursor()
    {
        // Create a gaze ray pointing forward from the camera
        Ray ray = new Ray(viewPoint.transform.position, viewPoint.transform.rotation * Vector3.forward);

        // reference variable to what we will hit...
        RaycastHit hit;

        // cast the ray cast out and return if we hit something...
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If the ray hits something, set the position to the hit point and rotate based on the normal vector of the hit
            cursorInstance.transform.position = hit.point;
            cursorInstance.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // If the ray doesn't hit anything, set the position to the maxGazeDistance and rotate to point away from the camera
            cursorInstance.transform.position = ray.origin + ray.direction.normalized * maxGazeDistance;
            cursorInstance.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
        }
    }
}
