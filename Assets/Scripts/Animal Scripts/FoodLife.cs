using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLife : MonoBehaviour
{
    public float health = 20f;

    private bool isEaten;

    private Rigidbody rigid;

    private Collider col;
    
    public void ApplyDamage(float damage)
    {
        if (isEaten)
            return;

        health -= damage;

        if (health <= 0f)
        {

            Eaten();

            isEaten = true;
        }
    }

    private void Eaten()
    {
        // as we store the foods in a given list for the animal to sort we turn off game object 
        // when life reaches 0, so the list has enough time to remove from the list, then we destory the
        // game object after some time
        gameObject.SetActive(false);
       
        if (!gameObject.activeSelf)
        {
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    { 

        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
