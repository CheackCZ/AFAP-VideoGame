using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the game flow, updating summary information, and handling scene transitions.
/// </summary>
public class Game_Manager : MonoBehaviour
{
    // References to the selection components
    public CarSelection carSelection;
    public MapSelection mapSelection; 
    public ReadInput readInput;

    // UI elements for the summary display
    public Image summaryCarImage;
    public TMP_Text summaryCarName;
    public Image summaryMapImage;
    public TMP_Text summaryMapName;
    public TMP_Text summaryNickname;

    /// <summary>
    /// Initializes the summary and subscribes to selection change events.
    /// </summary>
    void Start()
    {
        UpdateSummary();

        carSelection.onSelectionChanged += UpdateSummary;
        mapSelection.onSelectionChanged += UpdateSummary;
        readInput.onInputValid += UpdateSummary;
    }

    /// <summary>
    /// Updates the summary UI with the current selections.
    /// </summary>
    public void UpdateSummary()
    {
        int carIndex = carSelection.Index;

        summaryCarImage.sprite = carSelection.carImages[carIndex];
        summaryCarName.text = carSelection.carNames[carIndex];

        int mapIndex = mapSelection.Index;
        summaryMapImage.sprite = mapSelection.mapImages[mapIndex];
        summaryMapName.text = mapSelection.mapNames[mapIndex];

        summaryNickname.text = readInput.Input;

        Debug.Log("Summary Updated: CarIndex = " + carIndex + ", MapIndex = " + mapIndex + ", Nickname = " + readInput.Input);
    }

    /// <summary>
    /// Handles the race button press, storing the selected data and loading the game scene.
    /// </summary>
    public void OnRaceButtonPressed()
    {
        GameData.Instance.SelectedCarIndex = carSelection.Index;
        GameData.Instance.SelectedMapIndex = mapSelection.Index;
        GameData.Instance.PlayerName = readInput.Input;

        Debug.Log("OnRaceButtonPressed: CarIndex = " + GameData.Instance.SelectedCarIndex + ", MapIndex = " + GameData.Instance.SelectedMapIndex + ", Nickname = " + GameData.Instance.PlayerName);

        SceneManager.LoadScene("2.Game");
    }
}