using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;

/// <summary>
/// Manages the final scene, displaying race results and logging data to a CSV file.
/// </summary>
public class FinalSceneManager : MonoBehaviour
{
    // UI elements to display race time and username.
    public TMP_Text raceTimeText;
    public TMP_Text usernameText;

    /// <summary>
    /// Initializes the final scene by displaying race results and logging data.
    /// </summary>
    private void Start()
    {
        // Retrieve data from GameData.
        string username = GameData.Instance.PlayerName;
        float raceTime = GameData.Instance.RaceTime;
        string carName = GameData.Instance.SelectedCarName;
        string mapName = GameData.Instance.SelectedMapName;

        Debug.Log($"Username: {username}, Race Time: {raceTime}, Car Name: {carName}, Map Name: {mapName}");

        // Display race time in the UI.
        if (raceTimeText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(raceTime);
            string formattedTime = string.Format("{0}:{1:00}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
            raceTimeText.text = formattedTime;
        }

        // Display congratulatory message with the username.
        if (usernameText != null)
        {
            usernameText.text = "Congratulations, " + username;
        }

        // Log the race data to CSV.
        CSVLogger logger = FindObjectOfType<CSVLogger>();
        if (logger != null)
        {
            logger.LogRaceData(username, raceTime, carName, mapName);
        }
        else
        {
            Debug.LogError("CSVLogger not found in the scene!");
        }
    }
}
