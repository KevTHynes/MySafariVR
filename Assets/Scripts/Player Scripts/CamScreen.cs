using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScreen : MonoBehaviour
{
    private GameObject held_Cam;

    private GameObject cam_Lens;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        cam.enabled = false;
    }

    public bool Ready()
    {
        return true;
    }

    private void TurnOnScreen()
    {
        cam.enabled = true;
    }

    private void TurnOffScreen()
    {   
        if (cam.enabled)
        {
            cam.enabled = false;
            Debug.Log("The screen is still on");
        }
        
    }
    
}
