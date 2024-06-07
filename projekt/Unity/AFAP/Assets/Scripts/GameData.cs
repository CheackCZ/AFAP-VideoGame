using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Singleton class to store game data across scenes.
    /// </summary>
    public class GameData : MonoBehaviour
    {
        // Singleton instance
        public static GameData Instance;

        // Selected indexes
        public int SelectedCarIndex;
        public int SelectedMapIndex;

        // Selected names
        public string SelectedCarName;
        public string SelectedMapName;

        // Player's name
        public string PlayerName;

        // Race time
        public float RaceTime;

        // Flag to disable car at the start
        public bool DisableCarAtStart = true;

        /// <summary>
        /// Ensures a single instance of GameData persists across scenes.
        /// </summary>
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
