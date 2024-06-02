using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    public Sprite[] carImages;
    public string[] carNames;

    public Image carImageDisplay;
    public TMP_Text carNameDisplay;

    public Button next;
    public Button prev;

    private int index;


    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex", 0);

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        cars[index].SetActive(true);

        carImageDisplay.sprite = carImages[index];
        carNameDisplay.text = carNames[index];

        PlayerPrefs.SetString("CarName", carNames[index]);
        PlayerPrefs.SetString("CarImage", carImages[index].name);
    }

    void Update()
    {
        next.interactable = index < cars.Length - 1;
        prev.interactable = index > 0;
    }

    public void Next()
    {
        if (index < cars.Length - 1)
        {
            cars[index].SetActive(false);

            index++;

            cars[index].SetActive(true);

            carImageDisplay.sprite = carImages[index];
            carNameDisplay.text = carNames[index];

            PlayerPrefs.SetInt("carIndex", index);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetString("CarName", carNames[index]);
        PlayerPrefs.SetString("CarImage", carImages[index].name);
    }

    public void Prev()
    {
        if (index > 0)
        {
            cars[index].SetActive(false);

            index--;

            cars[index].SetActive(true);

            carImageDisplay.sprite = carImages[index];
            carNameDisplay.text = carNames[index];

            PlayerPrefs.SetInt("carIndex", index);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("CarName", carNames[index]);
            PlayerPrefs.SetString("CarImage", carImages[index].name);
        }
    }
}
