using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Manages logging race data to a CSV file.
/// </summary>
public class CSVLogger : MonoBehaviour
{
    // Path to the CSV log file.
    private string logFilePath;

    /// <summary>
    /// Initializes the logger by setting up the log file path and ensuring the file has headers.
    /// </summary>
    private void Start()
    {
        // Set the relative file path for the CSV log.
        logFilePath = GetRelativeLogFilePath();
        Debug.Log("Log file path: " + logFilePath);

        // Ensure the CSV file has headers if it doesn't exist.
        if (!File.Exists(logFilePath))
        {
            Debug.Log("Creating new log file with headers.");
            string headers = "Username,Race Time,Car Name,Map Name,Date";
            File.WriteAllText(logFilePath, headers + Environment.NewLine);
        }
    }

    /// <summary>
    /// Gets the relative path to the log file within the Assets folder.
    /// </summary>
    /// <returns>The relative path to the log file.</returns>
    private string GetRelativeLogFilePath()
    {
        // Get the relative path to the Assets folder and append the file name.
        string relativePath = Path.Combine(Application.dataPath, "RaceLog.csv");
        return relativePath;
    }

    /// <summary>
    /// Logs race data to the CSV file.
    /// </summary>
    /// <param name="username">The username of the player.</param>
    /// <param name="raceTime">The race time in seconds.</param>
    /// <param name="carName">The name of the car used in the race.</param>
    /// <param name="mapName">The name of the map where the race took place.</param>
    public void LogRaceData(string username, float raceTime, string carName, string mapName)
    {
        Debug.Log("Logging race data...");

        // Format the race time as mm:ss.fff.
        string formattedTime = TimeSpan.FromSeconds(raceTime).ToString(@"mm\:ss\.fff");

        // Create a log entry string.
        string logEntry = $"{username},{formattedTime},{carName},{mapName},{DateTime.Now}";
        Debug.Log("Log Entry: " + logEntry);

        try
        {
            // Append the log entry to the CSV file.
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            Debug.Log("Data logged successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to log data: " + ex.Message);
        }
    }
}
