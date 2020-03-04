using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WolfAchievements : MonoBehaviour
{
    [SerializeField]
    private Image a1_Complete, a1_Incomplete, a2_Complete, a2_Incomplete;


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
        
        if (Achievements.feed_Wolf == false)
        {
            FeedWolfIncomplete();
        }
        else if (Achievements.feed_Wolf == true)
        {
            FeedWolfComplete();
        }

        if (Achievements.wolf_Photo == false)
        {
            PhotoWolfIncomplete();
        }
        else if (Achievements.wolf_Photo == true)
        {
            PhotoWolfComplete();
        }
    }

    public void ShowAchievementsList()
    {
        gameObject.SetActive(true);
    }

    public void FeedWolfComplete()
    {
        a1_Complete.enabled = true;
        a1_Incomplete.enabled = false;
    }

    public void FeedWolfIncomplete()
    {
        a1_Complete.enabled = false;
        a1_Incomplete.enabled = true;
    }

    public void PhotoWolfComplete()
    {
        a2_Complete.enabled = true;
        a2_Incomplete.enabled = false;
    }

    public void PhotoWolfIncomplete()
    {
        a2_Complete.enabled = false;
        a2_Incomplete.enabled = true;
    }
}
