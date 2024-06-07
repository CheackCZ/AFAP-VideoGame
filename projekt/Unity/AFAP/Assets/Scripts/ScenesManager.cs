using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages scene transitions and application quitting.
/// </summary>
public class ScenesManager : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene in the build index.
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Debug.Log("quit");

        Application.Quit();
    }

    /// <summary>
    /// Loads the main menu scene (build index 0).
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene((0));
    }

    /// <summary>
    /// Loads the race scene asynchronously.
    /// </summary>
    public void Race()
    {
        SceneManager.LoadSceneAsync("6. Game");
    }
}
