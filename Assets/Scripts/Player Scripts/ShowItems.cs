using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItems : MonoBehaviour {

    public Transform itemSpawn;
    public Rigidbody throw_Meat;
    public Rigidbody thro_Fruit;
    public float thrownSpeed;

    private CameraAnimator camera_anim;


    [SerializeField]
    public GameObject meat_Inventory;
    [SerializeField]
    public GameObject camera_Inventory;
    [SerializeField]
    public GameObject fruit_Inventroy;

    private bool show_Meat_Inventory;
    private bool show_Camera_Inventory;
    private bool show_Fruit_Inventory;

    [SerializeField]
    public GameObject meat_Held;
    [SerializeField]
    public GameObject camera_Held;
    [SerializeField]
    public GameObject fruit_Held;

    private bool hold_Meat;
    private bool hold_Camera;
    private bool hold_Fruit;

    public bool inHand = false;

    private float animation_Time = 2.5f;

    private void Awake()
    {
        camera_anim = GetComponentInChildren<CameraAnimator>();
    }
    // Use this for initialization
    void Start () {

        show_Meat_Inventory = false;
        show_Camera_Inventory = false;
        show_Fruit_Inventory = false;
        CheckInventory();

        hold_Meat = false;
        hold_Camera = false;
        hold_Fruit = false;
        CheckHeldItem();

    }//end start method

    
    // Update is called once per frame
    void Update () {
        //bool down = Input.GetButtonDown("Items");
        bool held = Input.GetButton("Items");
        bool up = Input.GetButtonUp("Items");

        //call the inventory/holding items methods and evaluate items
        CheckInventory();

        CheckHeldItem();

        CheckInHand();

        //button held down makes inventory items visible
        if (held)
        {
            show_Meat_Inventory = true;
            show_Camera_Inventory = true;
            show_Fruit_Inventory = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //this is where we look at which item we want to hold
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Inventory Meat")
                {
                    //Debug.Log("This is Meat!");
                    hold_Meat = true;
                    hold_Camera = false;
                    hold_Fruit = false;
                }
                else if (hit.collider.tag == "Inventory Camera")
                {
                    //Debug.Log("This is a Camera!");
                    hold_Camera = true;
                    hold_Meat = false;
                    hold_Fruit = false;
                }
                else if (hit.collider.tag == "Inventory Fruit")
                {
                    //Debug.Log("This is Fruit!");
                    hold_Fruit = true;
                    hold_Meat = false;
                    hold_Camera = false;
                }       
            }
        }
        //button released makes inventory dissapear 
        else if (up)
        {
            show_Meat_Inventory = false;
            show_Camera_Inventory = false;
            show_Fruit_Inventory = false;

        }
        //statement to remove any item from hand
        else if (Input.GetButton("No Item"))
        {
            if (hold_Camera)
            {
                StartCoroutine(PutCameraAway());

            }
            else if (hold_Meat)
            {
                hold_Meat = false;
            }
            else if (hold_Fruit)
            {
                hold_Fruit = false;
            }
            /*
            else
            {
                hold_Meat = false;
                hold_Fruit = false;
            }
            */
        }

        //Function to throw first throwable item
        if (hold_Meat)
        {
            if (Input.GetButton("Fire1"))
            {
                hold_Meat = false;
                Rigidbody itemRigidbody1;
                itemRigidbody1 = Instantiate(throw_Meat, itemSpawn.position, itemSpawn.rotation) as Rigidbody;
                itemRigidbody1.AddForce(itemSpawn.forward * thrownSpeed);
            }
        }
        //Function to throw second throwable item
        else if (hold_Fruit)
        {
            if (Input.GetButton("Fire1"))
            {
                hold_Fruit = false;
                Rigidbody itemRigidbody2;
                itemRigidbody2 = Instantiate(thro_Fruit, itemSpawn.position, itemSpawn.rotation) as Rigidbody;
                itemRigidbody2.AddForce(itemSpawn.forward * thrownSpeed);
            }
        }
    }//end update method

    private void OpenInventory()
    {
        show_Meat_Inventory = true;
        show_Camera_Inventory = true;
        show_Fruit_Inventory = true;
    }

    //Method to turn iventory items on/off
    void CheckInventory()
    {
        //Checks Boolean, then turns items on/off
        if (show_Meat_Inventory == false)
        {
            meat_Inventory.SetActive(false);
        }
        else if (show_Meat_Inventory == true)
        {
            meat_Inventory.SetActive(true);
        }


        if (show_Camera_Inventory == false)
        {
            camera_Inventory.SetActive(false);
        }
        else if (show_Camera_Inventory == true)
        {
            camera_Inventory.SetActive(true);
        }


        if (show_Fruit_Inventory == false)
        {
            fruit_Inventroy.SetActive(false);
        }
        else if (show_Fruit_Inventory == true)
        {
            fruit_Inventroy.SetActive(true);
        }

    }

    //Method to turn held items on/off
    void CheckHeldItem()
    {
        //Checks Boolean, then turns held items on/off
        if (hold_Meat == false)
        {
            meat_Held.SetActive(false);
        }
        else if (hold_Meat == true)
        {
            meat_Held.SetActive(true);
        }

        if (hold_Camera == false)
        {
            camera_Held.SetActive(false);

            camera_anim.TurnOn(false);

            camera_anim.TurnOff(false);

        }
        else if (hold_Camera == true)
        {
            camera_Held.SetActive(true);

            camera_anim.TurnOn(true);

            //camera_anim.TurnOff(false);

        }

        if (hold_Fruit == false)
        {
            fruit_Held.SetActive(false);
        }
        else if (hold_Fruit == true)
        {
            fruit_Held.SetActive(true);
        }
    }
    //Method to remove items from hand
    void CheckInHand()
    {
        if (hold_Meat == true)
        {
            inHand = true;
        }
        else if (hold_Camera == true)
        {
            inHand = true;
        }
        else if (hold_Fruit == true)
        {
            inHand = true;
        }
        else
        {
            inHand = false;
        }

        if (inHand == true)
        {
            //Debug.Log("The player is holding something");
        }
        else
        {
            //Debug.Log("The player is NOT holding anything");
        }

    }

    IEnumerator PutCameraAway()
    {
        camera_anim.TurnOn(false);
        camera_anim.TurnOff(true);
        yield return new WaitForSeconds(animation_Time);
        //camera_anim.TurnOff(false);
        hold_Camera = false;

    }

    
}