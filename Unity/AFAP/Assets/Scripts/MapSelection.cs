using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public GameObject[] maps; 
    public Sprite[] mapImages; 
    public string[] mapNames; 

    public Image mapImageDisplay; 
    public TMP_Text mapNameDisplay; 

    public Button next;
    public Button prev;

    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("mapIndex", 0); 

        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }

        maps[index].SetActive(true);
        
        mapImageDisplay.sprite = mapImages[index]; 
        mapNameDisplay.text = mapNames[index];

        PlayerPrefs.SetString("MapName", mapNames[index]);
        PlayerPrefs.SetString("MapImage", mapImages[index].name);
    }

    void Update()
    {
        next.interactable = index < maps.Length - 1;
        prev.interactable = index > 0;
    }

    public void Next()
    {
        if (index < maps.Length - 1)
        {
            maps[index].SetActive(false);

            index++;
            
            maps[index].SetActive(true);
            
            mapImageDisplay.sprite = mapImages[index]; 
            mapNameDisplay.text = mapNames[index]; 
            
            PlayerPrefs.SetInt("mapIndex", index);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("MapName", mapNames[index]);
            PlayerPrefs.SetString("MapImage", mapImages[index].name);
        }
    }

    public void Prev()
    {
        if (index > 0)
        {
            maps[index].SetActive(false);

            index--;
            
            maps[index].SetActive(true);
            
            mapImageDisplay.sprite = mapImages[index]; 
            mapNameDisplay.text = mapNames[index]; 
            
            PlayerPrefs.SetInt("mapIndex", index);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("MapName", mapNames[index]);
            PlayerPrefs.SetString("MapImage", mapImages[index].name);
        }
    }
}
