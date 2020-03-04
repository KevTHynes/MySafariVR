using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementLoader : MonoBehaviour
{

    [SerializeField]
    private bool load_Croc_Achievements, load_Bear_Achievements, load_Wolf_Achievements, load_Marine_Achievements;

    // Achievement scrolls and script declaration
    [SerializeField]
    private GameObject croc_Scroll, bear_Scroll, wolf_Scroll, marine_Scroll;

    private CrocAchievements croc_Ach;
    private BearAchievements bear_Ach;
    private WolfAchievements wolf_Ach;
    private MarineAchievements mar_Ach;


    private void Awake()
    {
        croc_Ach = croc_Scroll.GetComponent<CrocAchievements>();
        bear_Ach = bear_Scroll.GetComponent<BearAchievements>();
        wolf_Ach = wolf_Scroll.GetComponent<WolfAchievements>();
        mar_Ach = marine_Scroll.GetComponent<MarineAchievements>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (load_Croc_Achievements)
            {
                croc_Ach.ShowAchievementsList();
            }

            if (load_Bear_Achievements)
            {
                bear_Ach.ShowAchievementsList();
            }

            if (load_Wolf_Achievements)
            {
                wolf_Ach.ShowAchievementsList();
            }

            if (load_Marine_Achievements)
            {
                mar_Ach.ShowAchievementsList();
            }

            //Debug.Log("The player passed through!");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent green cube at the transforms position
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
