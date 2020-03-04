using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOnRayCaster()
    {
        this.gameObject.SetActive(true);
    }

    public void TurnOffRayCaster()
    {
        this.gameObject.SetActive(false);
    }
}
