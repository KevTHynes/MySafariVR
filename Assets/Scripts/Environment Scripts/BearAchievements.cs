using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BearAchievements : MonoBehaviour
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
        if (Achievements.feed_Bear == false)
        {
            FeedBearIncomplete();
        }
        else if (Achievements.feed_Bear == true)
        {
            FeedBearComplete();
        }

        if (Achievements.bear_Photo == false)
        {
            PhotoBearIncomplete();
        }
        else if (Achievements.bear_Photo == true)
        {
            PhotoBearComplete();
        }
    }

    public void ShowAchievementsList()
    {
        gameObject.SetActive(true);
    }

    public void FeedBearComplete()
    {
        a1_Complete.enabled = true;
        a1_Incomplete.enabled = false;
    }

    public void FeedBearIncomplete()
    {
        a1_Complete.enabled = false;
        a1_Incomplete.enabled = true;
    }

    public void PhotoBearComplete()
    {
        a2_Complete.enabled = true;
        a2_Incomplete.enabled = false;
    }

    public void PhotoBearIncomplete()
    {
        a2_Complete.enabled = false;
        a2_Incomplete.enabled = true;
    }
}
