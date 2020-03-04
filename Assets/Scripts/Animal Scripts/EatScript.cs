using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    public float damage = 10f;
    public float radius = 1f;
    public LayerMask layerMask;

    void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (hits.Length > 0)
        {

            hits[0].gameObject.GetComponent<FoodLife>().ApplyDamage(damage);

            Debug.Log("The Animal took a bite");

            gameObject.SetActive(false);

        }
    }
}
