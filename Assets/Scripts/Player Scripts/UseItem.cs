using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    // Declaration of items in players hand
    [SerializeField]
    private GameObject meat_Held, fish_Held, camera_Held;

    // Declaration of gameobject where food will be fired from
    [SerializeField]
    private Transform itemSpawn;

    // Declaration of instatiated foods that will be thrown
    [SerializeField]
    private Rigidbody throw_Meat, throw_Fish;

    // Speed of thrown food
    public float thrownSpeed = 1000f;

    // Declaration of the inventory system and the hand held camera animations
    private InventoryController inventory;
    private CameraAnimator camera_anim;

    // Boolean to determine which animtion should be played for the hand held camera
    public bool cam_IsReady;

    private void Awake()
    {
        inventory = GetComponent<InventoryController>();
        camera_anim = GetComponentInChildren<CameraAnimator>();

    }// End Awake method

    // Start is called before the first frame update
    void Start()
    {
        
        TurnOffItems();

        cam_IsReady = false;

    }// End Start method

    // Update is called once per frame
    void Update()
    {
       
        CheckHeldItem();

    }// End Update method

    // Method for turning off all items in hand on Start 
    void TurnOffItems()
    {
        meat_Held.SetActive(false);
        fish_Held.SetActive(false);
        camera_Held.SetActive(false);

    }// End TurnOffItems method

    //Method to turn items in players hand on/off
    void CheckHeldItem()
    { 
        // if meat is/is not chosen from the inventory
        // switch it on/off
        if (inventory.hold_Meat == false)
        {
            meat_Held.SetActive(false);
        }
        else if (inventory.hold_Meat == true)
        {
            meat_Held.SetActive(true);

            ThrowMeat();
        }

        // if fish is/is not chosen from the inventory
        // switch it on/off
        if (inventory.hold_Fish == false)
        {
            fish_Held.SetActive(false);
        }
        else if (inventory.hold_Fish == true)
        {
            fish_Held.SetActive(true);

            ThrowFish();

        }

        // if camera is/is not chosen from the inventory
        // switch it on/off with the relevant animations
        if (inventory.hold_Camera == false && cam_IsReady)
        {
            StartCoroutine(TurnCameraOff());

        }
        else if (inventory.hold_Camera == true)
        {
            StartCoroutine(TurnCameraOn());

        }
        
    }// End CheckHeldItems method

    // Method for throwing the Meat food
    void ThrowMeat()
    {
        if (Input.GetButton("Fire1"))
        {
            // turn off hand held Meat once the food is thrown
            inventory.hold_Meat = false;

            Rigidbody meat;

            // Create a clone of the Meat prefab and throw it
            meat = Instantiate(throw_Meat, itemSpawn.position, itemSpawn.rotation) as Rigidbody;
            meat.AddForce(itemSpawn.forward * thrownSpeed);
        }

    }// End ThrowMeat method

    void ThrowFish()
    {
        if (Input.GetButton("Fire1"))
        {
            // turn off hand held Fish once the food is thrown
            inventory.hold_Fish = false;

            Rigidbody fish;

            // Create a clone of the Fish prefab and throw it
            fish = Instantiate(throw_Fish, itemSpawn.position, itemSpawn.rotation) as Rigidbody;
            fish.AddForce(itemSpawn.forward * thrownSpeed);
        }

    }// End ThrowFish method

    // Method for turning on the hand held camera with relavant animation
    IEnumerator TurnCameraOn()
    {
        //Debug.Log("TurnCameraOn() is called");

        camera_Held.SetActive(true);

        float animation_Time = 2.5f;

        yield return new WaitForSeconds(animation_Time);

        cam_IsReady = true;

    }// End TurnOnCamera method

    // Method for turning off camera with relavant animation
    IEnumerator TurnCameraOff()
    {
        //Debug.Log("TurnCameraOff() is called");
        float animation_Time = 2.5f;

        camera_anim.TurnOn(false);

        camera_anim.TurnOff(true);

        yield return new WaitForSeconds(animation_Time);

        camera_anim.TurnOff(false);

        cam_IsReady = false;

        camera_Held.SetActive(false);

    }// End TurnCameraOff method

} // End Class
