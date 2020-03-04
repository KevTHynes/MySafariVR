using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclsoureAudioHandler : MonoBehaviour
{
    private EnclosureAudio aud;

    public bool isInEnclosure, isOutEnclosure;

    private void Awake()
    {
        aud = GetComponentInChildren<EnclosureAudio>();

    }

    // Start is called before the first frame update
    void Start()
    {
        aud.PlaySound("Waves");
        //aud.PlaySound("Crickets");

        isInEnclosure = false;

        isOutEnclosure = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If we are inside the enclosure and making sure we not outside
        // lets play the relavant sound
        if (isInEnclosure && !isOutEnclosure)
        {
            Debug.Log("We currently inside the enclosure");

            aud.StopSound("Waves");
            aud.PlaySound("Crickets");
        }

        // Else if we are outside the enclosure and making sure we are definitely not inside still
        // lets play the relavant sound
        else if (!isInEnclosure && isOutEnclosure)
        {
            Debug.Log("We currently outside the enclosure");

            aud.StopSound("Crickets");
            aud.PlaySound("Waves");
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInEnclosure = true;
            isOutEnclosure = false;
        }
        


        //aud.StopSound("Waves");
        //aud.PlaySound("Crickets");
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInEnclosure = false;
            isOutEnclosure = true;
        }
        

        //aud.StopSound("Crickets");
        //aud.PlaySound("Waves");
    }
}
