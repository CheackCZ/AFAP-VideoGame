using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("quit");

        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene((0));
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void Pick(string username)
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "2.CarSelection")
        {
            string imageName = PlayerPrefs.GetString("CarImage");

            Debug.Log("Car: " + PlayerPrefs.GetString("CarName") + ", " + imageName);
        }
        
        if (activeSceneName == "3.MapSelection")
        {
            string imageName = PlayerPrefs.GetString("MapImage");

            Debug.Log("Map: " + PlayerPrefs.GetString("MapName") + ", " + imageName);
        }
        
        if (activeSceneName == "4.Nickname")
        {
            PlayerPrefs.SetString("Username", username);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }   


    public void Race()
    {
        SceneManager.LoadSceneAsync("6.Game");
    }

    public void Info()
    {
        SceneManager.LoadSceneAsync("Informations");
    }
}
