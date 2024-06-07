using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the speedometer needle's position based on the vehicle's speed.
/// </summary>
public class SpeedoMeter_Manager : MonoBehaviour
{
    // The GameObject representing the speedometer needle.
    public GameObject Needle;

    // Starting and ending positions for the needle rotation.
    private float startPosition = 216, endPosition = -41;
    private float desiredPosition;

    // The vehicle's current speed.
    [HideInInspector]
    public float vehicleSpeed;

    /// <summary>
    /// Called before the first frame update.
    /// </summary    
    void Start() { }


    /// <summary>
    /// Called once per frame at a fixed interval.
    /// Updates the needle position based on the vehicle's speed.
    /// </summary>    
    void FixedUpdate()
    {
        UpdateNeedle();
    }

    /// <summary>
    /// Updates the needle's rotation to reflect the current vehicle speed.
    /// </summary>
    private void UpdateNeedle()
    {
        // Calculate the desired position range for the needle.
        desiredPosition = startPosition - endPosition;

        // Normalize the vehicle speed to a value between 0 and 1 (assuming max speed is 180).
        float temp = vehicleSpeed / 180;

        // Set the needle's rotation based on the normalized speed.
        Needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
    }
}