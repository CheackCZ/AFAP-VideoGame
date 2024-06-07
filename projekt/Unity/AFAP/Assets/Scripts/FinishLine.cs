using UnityEngine;

/// <summary>
/// Manages the finish line functionality, checking if the car crosses the finish line twice to end the race.
/// </summary>
public class FinishLine : MonoBehaviour
{
    // Counter for the number of times the finish line has been crossed.
    private int crossCount = 0;

    /// <summary>
    /// Called when another collider enters the trigger collider attached to the object where this script is attached.
    /// </summary>
    /// <param name="other">The collider that enters the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a game object tagged as "Car".
        if (other.CompareTag("Car"))
        {
            // Increment the cross count.
            crossCount++;

            // Find the CountdownTimer component in the scene.
            CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
            if (countdownTimer != null)
            {
                // End the race if the car crosses the finish line twice.
                if (crossCount == 2)
                {
                    countdownTimer.EndRace();
                }
            }
        }
    }
}
