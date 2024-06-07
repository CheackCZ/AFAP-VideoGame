using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts;

/// <summary>
/// Handles the map selection logic.
/// </summary>
public class MapSelection : MonoBehaviour
{
    // Arrays of map fields
    public GameObject[] maps;
    public Sprite[] mapImages;
    public string[] mapNames;

    // UI elements to display the selected map's image and name
    public Image mapImageDisplay;
    public TMP_Text mapNameDisplay;

    // Buttons to navigate between maps
    public Button next;
    public Button prev;

    // Index of the currently selected map
    private int index;
    public int Index
    {
        get { return index; }
        set
        {
            index = value;
            GameData.Instance.SelectedMapName = mapNames[index];
            onSelectionChanged?.Invoke();
        }
    }

    // Event triggered when the map selection changes
    public event Action onSelectionChanged;

    /// <summary>
    /// Initializes the map selection to the first map.
    /// </summary>
    void Start()
    {
        Index = 0;

        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }

        maps[Index].SetActive(true);

        mapImageDisplay.sprite = mapImages[Index];
        mapNameDisplay.text = mapNames[Index];

        GameData.Instance.SelectedMapName = mapNames[Index]; // Initialize the selected map name
    }

    /// <summary>
    /// Updates the interactability of navigation buttons.
    /// </summary>
    void Update()
    {
        next.interactable = Index < maps.Length - 1;
        prev.interactable = Index > 0;
    }

    /// <summary>
    /// Selects the next map.
    /// </summary>
    public void Next()
    {
        if (Index < maps.Length - 1)
        {
            maps[Index].SetActive(false);
            Index++;
            maps[Index].SetActive(true);
            mapImageDisplay.sprite = mapImages[Index];
            mapNameDisplay.text = mapNames[Index];
        }
    }

    /// <summary>
    /// Selects the previous map.
    /// </summary>
    public void Prev()
    {
        if (Index > 0)
        {
            maps[Index].SetActive(false);
            Index--;
            maps[Index].SetActive(true);
            mapImageDisplay.sprite = mapImages[Index];
            mapNameDisplay.text = mapNames[Index];
        }
    }
}
