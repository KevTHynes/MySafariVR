using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDoorTrigger : MonoBehaviour
{
    private CaveAnimator anim;

    void Awake()
    {
        anim = GetComponentInParent<CaveAnimator>();
    }

    // Start is called before the first frame update
    /*
    void Start()
    {
        anim.OpenDoor();
    }
    */

    //method for checking the player has entered trigger zone, then open the door
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.OpenDoor();

        }
    }

}
