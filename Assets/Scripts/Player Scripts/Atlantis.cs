using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Atlantis : MonoBehaviour
{
    private FogMode fogMode;

    private float fogDensity;

    private Color fogColor;

    private bool fogEnabled;

    private Camera cam;

    private bool isInWater = false;

    private float waterSurfacePosY = 50.0f;

    public float aboveWaterTolerance = 0.5f;

    public Color fogColorWater;

    public PostProcessProfile land;

    public PostProcessProfile underwater;

    //public PostProcessVolume land;

    //public PostProcessVolume underwater;

    private GameObject aquarium;
    private EnclosureAudio aud;

    private void Awake()
    {
        aquarium = GameObject.FindGameObjectWithTag("Aquarium");
        aud = aquarium.GetComponentInChildren<EnclosureAudio>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        fogMode = RenderSettings.fogMode;
        fogDensity = RenderSettings.fogDensity;
        fogColor = RenderSettings.fogColor;
        fogEnabled = RenderSettings.fog;

        aud.PlaySound("Waves");

    }

    // Update is called once per frame
    void Update()
    {
        // Set underwater rendering or default
        if (IsUnderwater())
        {
            
            SetRenderDiving();
            
        }
        else
        {
            
            SetRenderDefault();
            
        }
    }

    // Check if we are underwater
    private bool IsUnderwater()
    {
        // we are under water when the players position is below the water surface
        return cam.gameObject.transform.position.y < (waterSurfacePosY);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            // We enter the water... doesn't matter if we return from underwater, we are still in the water
            isInWater = true;

            aud.StopSound("Waves");
            aud.PlaySound("Ambient");

            Debug.Log("Water Trigger Enter : " + isInWater);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Water" && isInWater)
        {
            
            // are we leaving the water, or are we under the surface?
            waterSurfacePosY = other.transform.position.y;
            float playerPosY = this.transform.position.y;
            if (playerPosY > waterSurfacePosY)
            {
                // ok we really left the water
                isInWater = false;

                aud.StopSound("Ambient");
                aud.PlaySound("Waves");
            }

            Debug.Log("Water Trigger Exit : " + isInWater);
        }
    }

    // Rendering when diving
    private void SetRenderDiving()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColorWater;
        RenderSettings.fogDensity = 0.015f;
        RenderSettings.fogMode = FogMode.ExponentialSquared;

        cam.GetComponent<PostProcessVolume>().profile = underwater;
        
    }

    // Rendering when above water
    private void SetRenderDefault()
    {
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fog = fogEnabled;
        RenderSettings.fogMode = fogMode;

        cam.GetComponent<PostProcessVolume>().profile = land;
        
    }
}
