using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;

    public bool inVR, notInVr = false;

    // Use this for initialization
    void Start ()
    {
        if (inVR || notInVr)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (notInVr)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);


            Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }

        if (inVR || notInVr)
        {
            if (Input.GetButtonDown("Cancel"))
                Cursor.lockState = CursorLockMode.None;
        }
        
    }

}