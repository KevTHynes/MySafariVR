using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAchievements : MonoBehaviour
{
    [SerializeField]
    private bool isCroc_Achievements, isBear_Achievements, isWolf_Achievements, isMarine_Aachievements;

    [SerializeField]
    private Image a1_Complete, a1_Incomplete;

    [SerializeField]
    private Image a2_Complete, a2_Incomplete;

    private GameObject ach_Manager;
    private AchievementsHandler ach_Handler;

    private bool achievement_1, achievement_2;

    private void Awake()
    {
        ach_Manager = GameObject.FindGameObjectWithTag("Achievements Handler");
        ach_Handler = ach_Manager.GetComponent<AchievementsHandler>();

        achievement_1 = false;

        achievement_2 = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        if (isCroc_Achievements)
        {
            SetupCrocAchievements();
        }

        if (isBear_Achievements)
        { 
            SetupBearAchievements();
        }

        if (isWolf_Achievements)
        {
            SetupWolfAchievements();
        }

        if (isMarine_Aachievements)
        {
            SetupMarineAchievements();
        }

        /*
        if (isCroc_Achievements)
        {
            a1_Complete.enabled = true;
            a1_Incomplete.enabled = false;

            a2_Complete.enabled = true;
            a2_Incomplete.enabled = false;
        }

        if (isBear_Achievements)
        {
            a1_Complete.enabled = false;
            a1_Incomplete.enabled = true;

            a2_Complete.enabled = false;
            a2_Incomplete.enabled = true;

        }
        */
    }

    private void SetupCrocAchievements()
    {
        if (!achievement_1)
        {
            a1_Complete.enabled = false;
            a1_Incomplete.enabled = true;

        }
        else if (achievement_1)
        {
            a1_Complete.enabled = true;
            a1_Incomplete.enabled = false;

        }

        if (!achievement_2)
        {
            a2_Complete.enabled = false;
            a2_Incomplete.enabled = true;

        }
        else if (achievement_2)
        {
            a2_Complete.enabled = true;
            a2_Incomplete.enabled = false;

        }
    }

    private void SetupBearAchievements()
    {

    }

    private void SetupWolfAchievements()
    {

    }

    private void SetupMarineAchievements()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
