using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Assets.Scripts;
using System;

public class ReadInput : MonoBehaviour
{
    private string input;

    public InputField inputField;
    public Button checkButton;

    public string pattern = @"^[a-zA-Z0-9]{2,}$"; 

    void Start()
    {
        checkButton.onClick.AddListener(CheckRegex);
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }

    public void CheckRegex()
    {
        string inputText = inputField.text;

        if (Regex.IsMatch(inputText, pattern))
        {
            Debug.Log("Input matches the pattern.");
            ScenesManager.Pick(inputText);
        }
        else
        {
            Debug.Log("Input does not match the pattern.");
        }
    }
}
