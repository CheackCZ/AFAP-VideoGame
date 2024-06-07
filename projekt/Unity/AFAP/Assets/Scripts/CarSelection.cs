using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the car selection logic.
/// </summary>
public class CarSelection : MonoBehaviour
{
    // Arrays of car fields
    public GameObject[] cars;
    public Sprite[] carImages;
    public string[] carNames;

    // UI elements to display the selected car's image and name
    public Image carImageDisplay;
    public TMP_Text carNameDisplay;

    // Buttons to navigate between cars
    public Button next;
    public Button prev;

    // Index of the currently selected car
    private int index;
    public int Index
    {
        get { return index; }
        set
        {
            index = value;
            GameData.Instance.SelectedCarName = carNames[index];
            onSelectionChanged?.Invoke();
        }
    }

    // Event triggered when the car selection changes
    public event Action onSelectionChanged;

    /// <summary>
    /// Initializes the car selection to the first car.
    /// </summary>
    void Start()
    {
        Index = 0;

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        cars[Index].SetActive(true);

        carImageDisplay.sprite = carImages[Index];
        carNameDisplay.text = carNames[Index];

        GameData.Instance.SelectedCarName = carNames[Index]; // Initialize the selected car name
    }

    /// <summary>
    /// Updates the interactability of navigation buttons.
    /// </summary>
    void Update()
    {
        next.interactable = Index < cars.Length - 1;
        prev.interactable = Index > 0;
    }

    /// <summary>
    /// Selects the next car.
    /// </summary>
    public void Next()
    {
        if (Index < cars.Length - 1)
        {
            cars[Index].SetActive(false);

            Index++;

            cars[Index].SetActive(true);

            carImageDisplay.sprite = carImages[Index];
            carNameDisplay.text = carNames[Index];
        }
    }

    /// <summary>
    /// Selects the previous car.
    /// </summary>
    public void Prev()
    {
        if (Index > 0)
        {
            cars[Index].SetActive(false);

            Index--;

            cars[Index].SetActive(true);

            carImageDisplay.sprite = carImages[Index];
            carNameDisplay.text = carNames[Index];
        }
    }
}
