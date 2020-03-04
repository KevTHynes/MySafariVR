using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MarineAchievements : MonoBehaviour
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

    public void ShowAchievementsList()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Achievements.shark_Photo == false)
        {
            PhotoSharkIncomplete();
        }
        else if (Achievements.shark_Photo == true)
        {
            PhotoSharkComplete();
        }

        if (Achievements.meg_Photo == false)
        {
            PhotoMegIncomplete();
        }
        else if (Achievements.meg_Photo == true)
        {
            PhotoMegComplete();
        }
    }

    public void PhotoSharkComplete()
    {
        a1_Complete.enabled = true;
        a1_Incomplete.enabled = false;
    }

    public void PhotoSharkIncomplete()
    {
        a1_Complete.enabled = false;
        a1_Incomplete.enabled = true;
    }

    public void PhotoMegComplete()
    {

        a2_Complete.enabled = true;
        a2_Incomplete.enabled = false;
    }

    public void PhotoMegIncomplete()
    {
        a2_Complete.enabled = false;
        a2_Incomplete.enabled = true;
    }
}
