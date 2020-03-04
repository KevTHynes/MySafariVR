using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodList : MonoBehaviour
{
    public List<Collider> foods;

    // Start is called before the first frame update
    void Start()
    {
        foods = new List<Collider>();
    }
}
