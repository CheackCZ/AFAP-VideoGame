using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages the initialization of the race scene by instantiating the selected car and map prefabs and setting player name.
    /// </summary>
    public class RaceManager : MonoBehaviour
    {
        /// <summary>
        /// Prefabs of all available cars and maps.
        /// </summary
        public GameObject[] carPrefabs;
        public GameObject[] mapPrefabs;

        /// <summary>
        /// Point where the selected car and map will be spawned.
        /// </summary>
        public Transform carSpawnPoint; 
        public Transform mapSpawnPoint;

        /// <summary>
        /// Text component displaying the player's name.
        /// </summary>
        public TMP_Text playerNameText;

        /// <summary>
        /// Initializes the race scene by instantiating the selected car and map prefabs and setting player name.
        /// </summary>
        void Start()
        {
            int selectedCarIndex = GameData.Instance.SelectedCarIndex;
            int selectedMapIndex = GameData.Instance.SelectedMapIndex;
            string playerName = GameData.Instance.PlayerName;

            Debug.Log("RaceManager Start: CarIndex = " + selectedCarIndex + ", MapIndex = " + selectedMapIndex + ", PlayerName = " + playerName);
          
            // Instantiate selected car
            if (carPrefabs.Length > selectedCarIndex)
            {
                Instantiate(carPrefabs[selectedCarIndex], carSpawnPoint.position, carSpawnPoint.rotation);
            }
            else
            {
                Debug.LogError("Selected car index out of range");
            }

            // Instantiate selected map
            if (mapPrefabs.Length > selectedMapIndex)
            {
                Instantiate(mapPrefabs[selectedMapIndex], mapSpawnPoint.position, mapSpawnPoint.rotation);
            }
            else
            {
                Debug.LogError("Selected map index out of range");
            }

            // Set player name
            if (playerNameText != null)
            {
                playerNameText.text = playerName;
            }
        }
    }
}
