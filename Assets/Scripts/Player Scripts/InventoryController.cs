using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    private InventoryAnimator anim;

    private Camera viewPoint;

    private GazeCursor gaze;

    private bool is_Open, is_Closed;
    
    public bool hover_Meat, hover_Camera, hover_Fish;

    public bool hold_Meat, hold_Camera, hold_Fish;

    private bool held, release, cancel;

    private void Awake()
    {
        anim = inventory.GetComponent<InventoryAnimator>();

        gaze = GetComponentInChildren<GazeCursor>();

        viewPoint = Camera.main;

    }

    // Start is called before the first frame update
    void Start()
    {
        is_Open = false;

        CloseInventory();
        
        hold_Meat = false;
        hold_Fish = false;
        hold_Camera = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Controls      
        held = Input.GetButton("Items");
        release = Input.GetButtonUp("Items");
        cancel = Input.GetButton("No Item");

        if (held)
        {
            OpenInventory();

            if (!gaze.isOn)
            {
                gaze.isOn = true;
            }            
        }

        if (release)
        {
            if (hover_Meat)
            {
                hold_Meat = true;
                hold_Fish = false;
                hold_Camera = false;
            }
            else if (hover_Fish)
            {
                hold_Meat = false;
                hold_Fish = true;
                hold_Camera = false;
            }
            else if (hover_Camera)
            {
                hold_Meat = false;
                hold_Fish = false;
                hold_Camera = true;
            }

            CloseInventory();

            if (gaze.isOn)
            {
                gaze.isOn = false;
            }

        }

        if (cancel)
        {
            RemoveFromHand();
        }

        if (hover_Meat)
        {
            LookAtMeat();
        }

        if (hover_Fish)
        {
            LookAtFish();
        }

        if (hover_Camera)
        {
            LookAtCam();
        }
    }

   

    private void OpenInventory()
    {
        inventory.SetActive(true);

        ScrollItems();

    }

    private void CloseInventory()
    {
        hover_Meat = false;
        hover_Fish = false;
        hover_Camera = false;

        anim.HoverMeat(false);
        anim.HoverFish(false);
        anim.HoverCamera(false);


        inventory.SetActive(false);


    }

    private void ScrollItems()
    {
        // Calling the raycaster reference we use to look at objects
        RaycastHit hit;
        Ray ray = new Ray(viewPoint.transform.position, viewPoint.transform.rotation * Vector3.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Inventory Meat")
            {
                //Debug.Log("This is Meat!");

                hover_Meat = true;
                hover_Fish = false;
                hover_Camera = false;

            }
            else if (hit.collider.tag == "Inventory Fish")
            {
                //Debug.Log("This is Fish!");

                hover_Fish = true;
                hover_Meat = false;
                hover_Camera = false;
                
            }
            else if (hit.collider.tag == "Inventory Camera")
            {
                //Debug.Log("This is a Camera!");

                hover_Camera = true;
                hover_Meat = false;
                hover_Fish = false;

            }

        }
    }

    void LookAtMeat()
    {
        if (!hover_Fish || !hover_Camera)
        {
            anim.HoverFish(false);
            anim.HoverCamera(false);
        }

        anim.HoverMeat(true);

    }

    void LookAtFish()
    {
        if (!hover_Meat || !hover_Camera)
        {
            anim.HoverMeat(false);
            anim.HoverCamera(false);
        }

        anim.HoverFish(true);

    }

    void LookAtCam()
    {
        if (!hover_Meat || !hover_Fish)
        {
            anim.HoverMeat(false);
            anim.HoverFish(false);
        }

        anim.HoverCamera(true);

    }

    private void RemoveFromHand()
    {
        hold_Meat = false;
        hold_Fish = false;
        hold_Camera = false;
    }

    
}
