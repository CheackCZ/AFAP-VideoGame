using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Assets.Scripts;
using System;
using TMPro;

/// <summary>
/// Handles user input validation and panel navigation.
/// </summary>
public class ReadInput : MonoBehaviour
{
    // The user input string
    private string input;
    public string Input { get { return input; } set { input = value; } }

    // UI elements for input and validation
    public InputField inputField;
    public Button checkButton;
    public TMP_Text errorText;

    // Regex pattern for input validation
    public string pattern = @"^[a-zA-Z0-9]{2,}$";

    // GameObjects for managing current and next panels
    public GameObject currentPanel; 
    public GameObject nextPanel;

    // Event triggered when the input is valid
    public event Action onInputValid;

    /// <summary>
    /// Initializes the button click listener.
    /// </summary>
    void Start()
    {
        checkButton.onClick.AddListener(CheckRegex);
    }

    /// <summary>
    /// Reads the input string and stores it in the Input property.
    /// </summary>
    /// <param name="s">The input string.</param>
    public void ReadStringInput(string s)
    {
        Input = s;
        Debug.Log(Input);
    }

    /// <summary>
    /// Checks if the input text matches the regex pattern and handles panel transitions.
    /// </summary>
    public void CheckRegex()
    {
        string inputText = inputField.text;

        if (Regex.IsMatch(inputText, pattern))
        {
            Debug.Log("Input matches the pattern.");

            Input = inputText;

            onInputValid?.Invoke(); 

            nextPanel.SetActive(true);
            currentPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Input does not match the pattern.");
            errorText.text = "Username has to be at least 2 letters long and accepty only alphanumerical values.";
        }
    }
}
