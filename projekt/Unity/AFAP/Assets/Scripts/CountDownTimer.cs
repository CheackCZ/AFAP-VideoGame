using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using Assets.Scripts;

/// <summary>
/// Controls the countdown timer, race start sequence, and updates race-related UI elements.
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    // UI elements for countdown, username, live race time, and finish text.
    public TMP_Text countdownText;
    public TMP_Text usernameText;
    public TMP_Text liveRaceTimeText;
    public TMP_Text finishText;

    // Game objects for the speedometer and player info.
    public GameObject speedometer;
    public GameObject playerInfo;

    // Color to be used when the race ends.
    public Color endRaceColor = Color.red;

    // Variables to track race start time and race status.
    private float startTime;
    private bool raceOngoing;

    // Reference to the ScenesManager script.
    private ScenesManager scenesManager;

    /// <summary>
    /// Initializes the ScenesManager and sets up the initial state of UI elements.
    /// </summary>
    private void Start()
    {
        scenesManager = FindObjectOfType<ScenesManager>();
        if (scenesManager == null)
        {
            Debug.LogError("ScenesManager not found in the scene!");
        }

        if (speedometer != null) speedometer.SetActive(false);
        if (playerInfo != null) playerInfo.SetActive(false);

        if (countdownText != null)
        {
            StartCoroutine(CountdownRoutine());
        }
        else
        {
            Debug.LogError("Countdown Text is not assigned!");
        }

        if (finishText != null)
        {
            finishText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Coroutine to handle the countdown sequence and start the race.
    /// </summary>
    private IEnumerator CountdownRoutine()
    {
        int countdownValue = 3;

        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            StartCoroutine(AnimateText(countdownText));
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        countdownText.text = "GO!";
        StartCoroutine(AnimateText(countdownText));
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        if (speedometer != null) speedometer.SetActive(true);
        if (playerInfo != null) playerInfo.SetActive(true);

        GameData.Instance.DisableCarAtStart = false; // Allow the car to be controlled now
        Debug.Log("CountdownTimer: CountdownRoutine - Car control enabled");
        StartRace();
    }

    /// <summary>
    /// Coroutine to animate the countdown text.
    /// </summary>
    /// <param name="text">The text to animate.</param>
    private IEnumerator AnimateText(TMP_Text text)
    {
        float duration = 0.5f;
        float time = 0;

        Vector3 originalScale = text.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;

        while (time < duration)
        {
            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        text.transform.localScale = targetScale;

        time = 0;
        while (time < duration)
        {
            text.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        text.transform.localScale = originalScale;
    }

    /// <summary>
    /// Starts the race by setting the race start time and updating the username text.
    /// </summary>
    private void StartRace()
    {
        Debug.Log("Race Started!");

        if (usernameText != null)
        {
            usernameText.text = GameData.Instance.PlayerName;
        }

        startTime = Time.time;
        raceOngoing = true;
    }

    /// <summary>
    /// Updates the live race time text if the race is ongoing.
    /// </summary>
    private void Update()
    {
        if (raceOngoing)
        {
            float currentTime = Time.time - startTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            string formattedTime = string.Format("{0}:{1:00}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);

            if (liveRaceTimeText != null)
            {
                liveRaceTimeText.text = formattedTime;
            }
        }
    }

    /// <summary>
    /// Ends the race and updates the UI elements accordingly.
    /// </summary>
    public void EndRace()
    {
        if (raceOngoing)
        {
            float raceTime = Time.time - startTime;
            raceOngoing = false;
            Debug.Log("Race Ended! Time: " + raceTime + " seconds");

            TimeSpan timeSpan = TimeSpan.FromSeconds(raceTime);
            string formattedTime = string.Format("{0}:{1:00}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);

            if (liveRaceTimeText != null)
            {
                liveRaceTimeText.text = formattedTime;
                liveRaceTimeText.color = endRaceColor; // Set the color to endRaceColor
            }

            StopGame();

            // Store the race time in the GameData
            GameData.Instance.RaceTime = raceTime;

            // Show and animate the finish text
            if (finishText != null)
            {
                StartCoroutine(ShowFinishText());
            }

            // Start coroutine to wait for 3 seconds and then load the next scene
            StartCoroutine(WaitAndLoadNextScene());
        }
    }

    /// <summary>
    /// Stops the game, including any car controls.
    /// </summary>
    private void StopGame()
    {
        Debug.Log("Game Stopped!");
    }

    /// <summary>
    /// Coroutine to show and animate the finish text.
    /// </summary>
    private IEnumerator ShowFinishText()
    {
        finishText.gameObject.SetActive(true);
        finishText.text = "FINISH!";
        yield return AnimateText(finishText);
    }

    /// <summary>
    /// Coroutine to wait for a few seconds and then load the next scene.
    /// </summary>
    private IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(5f);
        scenesManager.LoadNextScene();
    }
}
