using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrocAchievements : MonoBehaviour
{
    [SerializeField]
    public Image a1_Complete, a1_Incomplete, a2_Complete, a2_Incomplete;

    private GameObject ach_Manager;
    private AchievementsHandler ach_Handler;

    private bool achievement_1, achievement_2;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
       
        a1_Complete.enabled = false;
        a1_Incomplete.enabled = false;
        a2_Complete.enabled = false;
        a2_Incomplete.enabled = false;
        
    }

    private void Update()
    {
        if (Achievements.feed_Croc == false)
        {
            FeedCrocIncomplete();
        }
        else if (Achievements.feed_Croc == true)
        {
            FeedCrocComplete();
        }

        if (Achievements.croc_Photo == false)
        {
            PhotoCrocIncomplete();
        }
        else if (Achievements.croc_Photo == true)
        {
            PhotoCrocComplete();
        }
    }

    public void ShowAchievementsList()
    {
        gameObject.SetActive(true);
    }
    

    public void FeedCrocComplete()
    {
        a1_Complete.enabled = true;
        a1_Incomplete.enabled = false;
    }

    public void FeedCrocIncomplete()
    {
        a1_Complete.enabled = false;
        a1_Incomplete.enabled = true;
    }

    public void PhotoCrocComplete()
    {
        a2_Complete.enabled = true;
        a2_Incomplete.enabled = false;
    }

    public void PhotoCrocIncomplete()
    {
        a2_Complete.enabled = false;
        a2_Incomplete.enabled = true;
    }
    

}
