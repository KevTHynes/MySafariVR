using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    // Instance of this class we will carry on throughout the game
    public static Achievements achievements;

    // Feeding animals achievements
    public static bool feed_Croc, feed_Bear, feed_Wolf = false;

    // Photo of animals feeding achievements
    public static bool croc_Photo, bear_Photo, wolf_Photo, shark_Photo, sharkEating_Photo, meg_Photo = false;


    private void Awake()
    {
        // If this class is not existent, create one!
        // otherwise kill it so we dont have duplicates
        if (achievements == null)
        {
            achievements = this;

            // Keep variables as they were as the player traverses through the scenes
            DontDestroyOnLoad(achievements);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
