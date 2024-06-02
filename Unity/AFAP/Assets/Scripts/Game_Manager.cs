using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private int carIndex;
    private int mapIndex;

    public GameObject[] cars;
    public GameObject[] maps;

    void Start()
    {
        carIndex = PlayerPrefs.GetInt("carIndex", 0); 
        mapIndex = PlayerPrefs.GetInt("mapIndex", 0);

        GameObject car = Instantiate(cars[carIndex], Vector3.zero, Quaternion.identity);
        GameObject map = Instantiate(maps[mapIndex], Vector3.zero, Quaternion.identity);
    }
}
