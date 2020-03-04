using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour {

    public Transform itemSpawn;
    public Rigidbody throw_Meat;
    public Rigidbody throw_Fruit;
    public float thrownSpeed;

    // Use this for initialization
    void Start () {
  
        //Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cart")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.name == "Bear")
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
