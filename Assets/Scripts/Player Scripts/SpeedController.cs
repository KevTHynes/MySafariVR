using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpeedController : MonoBehaviour
{
    private GameObject player;

    private CinemachineDollyCart cart;

    public float cartSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // this should change

        cart = player.GetComponentInParent<CinemachineDollyCart>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cart.m_Speed = cartSpeed;
        }

    }
}
