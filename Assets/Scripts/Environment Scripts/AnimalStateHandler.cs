using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStateHandler : MonoBehaviour
{
    public List<GameObject> animals = new List<GameObject>();
    
    private BearController bear_Info;

    private CrocControllerLand croc_Info;

    private AlphaWolfController wolf_Info;

    private SharkController shark_Info;

    private MegController meg_Info;

    public bool bear_Ready, croc_Ready, wolf_Ready, shark_Ready, meg_Ready;

    public bool bear_IsEating, croc_IsEating, wolf_IsEating, shark_IsEating;


    /*
     * *REMEMBER*
     * 
     * We need to make sure any animals that are inactive in scene need to be active at very begining
     * so that this class can capture the information on wake. Use code to turn objects on/off, not
     * in inspector
     * 
    */
    private void Awake()
    {
        // once each animal is active in the given scene, get the details from their
        // individual scripts so that we can reference them later...

        if (animals != null)
        {
            foreach(GameObject animal in animals){

                if (animal.tag == "Croc")
                {
                    Debug.Log("The Croc is Active!");

                    croc_Info = animal.GetComponent<CrocControllerLand>();
                    croc_Ready = true;
                }
                else if (animal.tag == "Bear")
                {
                    Debug.Log("The Bear is Active!");

                    bear_Info = animal.GetComponent<BearController>();

                    bear_Ready = true;
                }
                else if (animal.tag == "AlphaWolf")
                {
                    Debug.Log("The Wolf is Active!");

                    wolf_Info = animal.GetComponent<AlphaWolfController>();
                    wolf_Ready = true;
                }
                else if (animal.tag == "GreatWhite")
                {
                    Debug.Log("The Great White is Active!");

                    shark_Info = animal.GetComponent<SharkController>();
                    shark_Ready = true;
                }
                else if (animal.tag == "Megaladon")
                {
                    Debug.Log("The Megaladon is Active!");

                    meg_Info = animal.GetComponent<MegController>();
                    meg_Ready = true;
                }
            }
        }
    }

    void Start()
    {
        if (animals != null)
        {
            foreach (GameObject animal in animals)
            {
                animal.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if (bear_Ready)
        {
            GetBearState();
        }

        if (croc_Ready)
        {
            GetCrocState();
        }

        if (wolf_Ready)
        {
            GetWolfState();
        }

        //Debug.Log("The animal " + animals.ToString() + "Is active");
    }

    private void GetBearState()
    {
        if (bear_Info.is_Eating)
        {
            Debug.Log("The Bear is Eating, Take the picture!");
            bear_IsEating = true;
        }
    }

    private void GetCrocState()
    {   
        if (croc_Info.is_Eating)
        {
            Debug.Log("The Crocodile is Eating, Take the picture!");
            croc_IsEating = true;
        }
        
    }

    private void GetWolfState()
    {        
        if (wolf_Info.is_Eating)
        {
            Debug.Log("The Wolf is Eating, Take the picture!");
            wolf_IsEating = true;
        }
        
    }

    //method for checking the player has entered trigger zone, then open the door
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (animals != null)
            {
                foreach (GameObject animal in animals)
                {
                    animal.SetActive(true);
                }
            }

        }
    }
}
