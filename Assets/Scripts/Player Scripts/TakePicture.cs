using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{

    private GameObject cam;
    private Camera cam_Lens;
    private float lens_Range = 100f;

    private AudioSource cam_Audio;

    //private ShowItems inventory;

    private UseItem inventory;


    private CamScreen cam_Screen;

    private GameObject animal_Handler;
    private AnimalStateHandler animal_Info;

    public bool croc_Captured, bear_Captured, wolf_Captured, shark_Captured, sharkEating_Captured, meg_Captured;

    private bool isTaken;

    //private CameraClearFlags clearFlags;


    // We need to catch the Camera gameobject details before it goes inactive
    private void Awake()
    {
        //cam = GameObject.FindGameObjectWithTag("CameraLens");

        // Call in the necessary components belonging to the camera
        cam = GameObject.FindGameObjectWithTag("HeldCamera");
        cam_Lens = cam.GetComponentInChildren<Camera>();
        cam_Screen = cam.GetComponentInChildren<CamScreen>();
        cam_Audio = cam.GetComponent<AudioSource>();

        //Debug.Log("we have the object: " + cam.name);
        //Debug.Log("we have the object: " + cam_Lens.name);

        // Call in the object and class which contains the animals information
        animal_Handler = GameObject.FindGameObjectWithTag("AnimalHandler");
        animal_Info = animal_Handler.GetComponent<AnimalStateHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isTaken = false;

        //Old script
        //inventory = GetComponent<ShowItems>();

        inventory = GetComponent<UseItem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the camera is on and held by player
        if (inventory.cam_IsReady)
        {
            if (!cam_Screen.Ready())
                return;

            if (isTaken)
                return;

            if (Input.GetButton("Fire1"))
            {
                Debug.Log("Click");

                Play_ClickSound();
                StartCoroutine(CapturePicture());
                AnalyzeCapture();
                return;
            }

        }
    }

    IEnumerator CapturePicture()
    {
        int origin_Culling = cam_Lens.cullingMask;

        isTaken = true;
        cam_Lens.clearFlags = CameraClearFlags.Nothing;
        cam_Lens.cullingMask = 0;

        yield return new WaitForSeconds(.5f);

        isTaken = false;
        cam_Lens.clearFlags = CameraClearFlags.Skybox;
        cam_Lens.cullingMask = origin_Culling;
    }

    void Play_ClickSound()
    {
        cam_Audio.Play();
    }

    private void AnalyzeCapture()
    {
        // Only cast ray on layer 10 (Animal layer)
        int layerMask = 1 << 10;
        
        RaycastHit hit;

        // Check that the lens is pointing at the animal
        if (Physics.Raycast(cam_Lens.transform.position, cam_Lens.transform.forward, out hit, lens_Range, layerMask))
        {
            Debug.Log("The picture contains an animal");

            // Check to see if the animal is eating
            if (animal_Info.bear_IsEating)
            {
                Debug.Log("Great picture!!! you caught the Bear eating :) ");
                bear_Captured = true;
            }
            else if (animal_Info.croc_IsEating)
            {
                Debug.Log("Great picture!!! you caught the Crocodile eating :) ");
                croc_Captured = true;
            }
            else if (animal_Info.wolf_IsEating)
            {
                Debug.Log("Great picture!!! you caught the Wolf eating :) ");
                wolf_Captured = true;
            }
            else if (hit.collider.tag == "GreatWhite")
            {
                Debug.Log("Great picture of the Great White!");
                shark_Captured = true;
            }
            else if (hit.collider.tag == "Megaladon")
            {
                Debug.Log("Great picture of the rare Megaladon!");
                meg_Captured = true;
            }
            else
            {
                Debug.Log("You didnt quite get the right picture :( ");
            }
        }
        else
        {
            Debug.Log("No animals are in this picture!");
        }

        /*
        if (animal_Info.bear_IsEating)
        {
            Debug.Log("Great picture!!! you caught the Bear eating :) ");
        }
        else if (animal_Info.croc_IsEating)
        {
            Debug.Log("Great picture!!! you caught the Crocodile eating :) ");
        }
        else if (animal_Info.wolf_IsEating)
        {
            Debug.Log("Great picture!!! you caught the Crocodile eating :) ");
        }
        else if (animal_Info.shark_IsEating)
        {
            Debug.Log("Great picture!!! you caught the Crocodile eating :) ");
        }
        else
        {
            Debug.Log("You didnt quite get the right picture :( ");
        }
        */
    }
}
