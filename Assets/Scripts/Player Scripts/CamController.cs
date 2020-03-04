using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Camera cam_Lens;
    // Start is called before the first frame update
    void Start()
    {
        cam_Lens = GameObject.Find("Lens").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
