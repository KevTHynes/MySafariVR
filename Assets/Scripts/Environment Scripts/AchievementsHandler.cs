using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsHandler : MonoBehaviour
{
    
    // Achievement scrolls and script declaration
    private GameObject croc_Scroll, bear_Scroll, wolf_Scroll, marine_Scroll;

    private CrocAchievements croc_Ach;
    private BearAchievements bear_Ach;
    private WolfAchievements wolf_Ach;
    private MarineAchievements mar_Ach;

    // Player's information declaration
    private GameObject player;
    private TakePicture photo;

    // Animal Information hanlder declaration
    private GameObject animal_Handler;
    private AnimalStateHandler animal_Info;

    // Feeding animals achievements
    private bool feed_Croc, feed_Bear, feed_Wolf = false;

    // Photo of animals feeding achievements
    private bool croc_Photo, bear_Photo, wolf_Photo, shark_Photo, sharkEating_Photo, meg_Photo = false;


    private void Awake()
    {
        // Reference Animal information
        animal_Handler = GameObject.FindGameObjectWithTag("AnimalHandler");
        animal_Info = animal_Handler.GetComponent<AnimalStateHandler>();

        // Reference players information
        player = GameObject.FindGameObjectWithTag("Player");
        photo = player.GetComponentInChildren<TakePicture>();

        croc_Scroll = GameObject.FindGameObjectWithTag("Croc Achievements");
        bear_Scroll = GameObject.FindGameObjectWithTag("Bear Achievements");
        wolf_Scroll = GameObject.FindGameObjectWithTag("Wolf Achievements");
        marine_Scroll = GameObject.FindGameObjectWithTag("Marine Achievements");

        // Reference animal achievements
        croc_Ach = croc_Scroll.GetComponent<CrocAchievements>();
        bear_Ach = bear_Scroll.GetComponent<BearAchievements>();
        wolf_Ach = wolf_Scroll.GetComponent<WolfAchievements>();
        mar_Ach = marine_Scroll.GetComponent<MarineAchievements>();

    }
    
    // Update is called once per frame
    void Update()
    {
        // If we havent fed the croc yet, lets make sure we are checking once we do feed it
        if (!feed_Croc)
        {

            CheckFeedCrocAchievement();
        }

        // If we havent photo the croc yet, lets make sure we are checking once we do
        if (!croc_Photo)
        {
            CheckCrocPhotoAchievement();
        }

        // If we havent fed the bear yet, lets make sure we are checking once we do feed it
        if (!feed_Bear)
        {
            CheckFeedBearAchievement();
        }

        // If we havent photo the croc yet, lets make sure we are checking once we do
        if (!bear_Photo)
        {
            CheckBearPhotoAchievement();
        }

        // If we havent fed the wolf yet, lets make sure we are checking once we do feed it
        if (!feed_Wolf)
        {
            CheckFeedWolfAchievement();
        }

        // If we havent photo the wolf yet, lets make sure we are checking once we do
        if (!wolf_Photo)
        {
            CheckWolfPhotoAchievement();
        }

        // If we havent photo the Great white yet, lets make sure we are checking once we do
        if (!shark_Photo)
        {
            CheckSharkPhotoAchievement();
        }

        // If we havent photo the Megaladon yet, lets make sure we are checking once we do
        if (!meg_Photo)
        {
            CheckMegPhotoAchievement();
        }
        
    }

    private void CheckFeedCrocAchievement()
    {
       // is the animal eating?
        if (animal_Info.croc_IsEating)
        {
            Debug.Log("CheckFeedCrocAchievement() Accessed...");

            // If so, dont check this method again
            feed_Croc = true;

            // Write to the acievements to check this one off the list
            Achievements.feed_Croc = true;
        }
    }

    private void CheckCrocPhotoAchievement()
    {
        // Did we take the photo at the right time?
        if (photo.croc_Captured)
        {
            Debug.Log("CheckCrocPhotoAchievement() Accessed...");

            // If so, dont check this method again
            croc_Photo = true;

            // Write to the acievements to check this one off the list
            Achievements.croc_Photo = true;
        }
    }

    private void CheckFeedBearAchievement()
    {
        // is the animal eating?
        if (animal_Info.bear_IsEating)
        {
            Debug.Log("CheckFeedBearAchievement() Accessed...");

            // If so, dont check this method again
            feed_Bear = true;

            // Write to the acievements to check this one off the list
            Achievements.feed_Bear = true;
        }
    }

    private void CheckBearPhotoAchievement()
    {
        // Did we take the photo at the right time?
        if (photo.bear_Captured)
        {
            Debug.Log("CheckBearPhotoAchievement() Accessed...");

            // If so, dont check this method again
            bear_Photo = true;

            // Write to the acievements to check this one off the list
            Achievements.bear_Photo = true;

        }
    }

    private void CheckFeedWolfAchievement()
    {
        // is the animal eating?
        if (animal_Info.wolf_IsEating)
        {
            Debug.Log("CheckFeedWolfAchievement() Accessed...");

            // If so, dont check this method again
            feed_Wolf = true;

            // Write to the acievements to check this one off the list
            Achievements.feed_Wolf = true;
        }
    }

    private void CheckWolfPhotoAchievement()
    {
        // Did we take the photo at the right time?
        if (photo.wolf_Captured)
        {
            Debug.Log("CheckWolfPhotoAchievement() Accessed...");

            // If so, dont check this method again
            wolf_Photo = true;

            // Write to the acievements to check this one off the list
            Achievements.wolf_Photo = true;
        }
    }

    private void CheckSharkPhotoAchievement()
    {
        // Did we take the photo?
        if (photo.shark_Captured)
        {
            Debug.Log("CheckSharkPhotoAchievement() Accessed...");

            // If so, dont check this method again
            shark_Photo = true;

            // Write to the acievements to check this one off the list
            Achievements.shark_Photo = true;
        }
    }

    private void CheckMegPhotoAchievement()
    {
        // Did we take the photo?
        if (photo.meg_Captured)
        {
            Debug.Log("CheckMegPhotoAchievement() Accessed...");

            // If so, dont check this method again
            meg_Photo = true;

            // Write to the acievements to check this one off the list
            Achievements.meg_Photo = true;
        }
    }
}
