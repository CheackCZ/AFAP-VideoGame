using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

/// <summary>
/// Controls the car's movement, handling, and sound effects.
/// Skript je od pana učitele Papuli.
/// </summary>
public class CarController : MonoBehaviour
{
    // Enumeration for the driven axle types.
    private enum DrivenAxle { front, back, both };

    // Constants for input axis names.
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    // Input values.
    private float horizontalInput;
    private float verticalInput;

    // Car's steering and braking parameters.
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    // Rigidbody component reference.
    private Rigidbody _rb;

    [Space(30)]
    [Header("Nastavení auta")]
    [SerializeField] private DrivenAxle drivenAxle;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [Space(30)]
    [Header("Zvuky")]
    [SerializeField] private AudioSource motorAudio;
    [SerializeField] private float minimumPitch = 0.75f;
    [SerializeField] private float maximumPitch = 2f;

    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float minSpeed = 0.3f;

    [Space(30)]
    [Header("Levé přední kolo")]
    [SerializeField] private WheelCollider leftFrontCollider;
    [SerializeField] private Transform leftFrontTransform;

    [Space(30)]
    [Header("Pravé přední kolo")]
    [SerializeField] private WheelCollider rightFrontCollider;
    [SerializeField] private Transform rightFrontTransform;

    [Space(30)]
    [Header("Levé zadní kolo")]
    [SerializeField] private WheelCollider leftRearCollider;
    [SerializeField] private Transform leftRearTransform;

    [Space(30)]
    [Header("Pravé zadní kolo")]
    [SerializeField] private WheelCollider rightRearCollider;
    [SerializeField] private Transform rightRearTransform;

    // Current speed and pitch values.
    public float currentSpeed;
    private float currentPitch;

    // Audio source and clip for test sounds.
    public AudioSource testSound;
    public AudioClip testClip;

    // Reference to the speedometer manager
    private SpeedoMeter_Manager speedoMeterManager;

    // Flag to enable or disable the car controller.
    private bool isEnabled;

    /// <summary>
    /// Initializes the Rigidbody component and sets up the initial state of the car.
    /// </summary>
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();

        // Find the SpeedoMeter_Manager component in the scene if not assigned
        speedoMeterManager = FindObjectOfType<SpeedoMeter_Manager>();

        if (speedoMeterManager == null)
        {
            Debug.LogError("SpeedoMeter_Manager not found in the scene!");
        }

        // Disable the car controller if the flag is set
        if (GameData.Instance.DisableCarAtStart)
        {
            Debug.Log("CarController: Start - Disabling CarController");
            isEnabled = false;
        }
        else
        {
            isEnabled = true;
        }
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame.
    /// Handles input, motor, steering, and wheel updates.
    /// </summary>
    private void FixedUpdate()
    {
        if (isEnabled)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
            UpdateSpeedometer();
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// Handles engine sound and test sounds.
    /// </summary>
    private void Update()
    {
        // Re-enable the car controller when the flag is cleared
        if (!isEnabled && !GameData.Instance.DisableCarAtStart)
        {
            Debug.Log("CarController: Update - Enabling CarController");
            isEnabled = true;
        }

        if (isEnabled)
        {
            EngineSound();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                testSound.Play();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioSource.PlayClipAtPoint(testClip, this.transform.position);
            }
        }
    }

    /// <summary>
    /// Handles the engine sound based on the car's speed.
    /// </summary>
    private void EngineSound()
    {
        currentSpeed = Mathf.Abs(_rb.velocity.magnitude);
        currentPitch = Mathf.Abs(currentSpeed / 50f * verticalInput);

        if (currentSpeed < minSpeed)
        {
            motorAudio.pitch = minimumPitch;
        }
        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            motorAudio.pitch = minimumPitch + currentPitch;
        }
        if (currentPitch > maxSpeed)
        {
            motorAudio.pitch = maximumPitch;
        }
    }

    /// <summary>
    /// Gets the input from the player.
    /// </summary>
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    /// Handles the motor force and braking force.
    /// </summary>
    private void HandleMotor()
    {
        switch (drivenAxle)
        {
            case DrivenAxle.front:
                leftFrontCollider.motorTorque = verticalInput * motorForce;
                rightFrontCollider.motorTorque = verticalInput * motorForce;
                break;
            case DrivenAxle.back:
                leftRearCollider.motorTorque = verticalInput * motorForce;
                rightRearCollider.motorTorque = verticalInput * motorForce;
                break;
            case DrivenAxle.both:
                leftFrontCollider.motorTorque = verticalInput * motorForce / 2;
                rightFrontCollider.motorTorque = verticalInput * motorForce / 2;
                leftRearCollider.motorTorque = verticalInput * motorForce / 2;
                rightRearCollider.motorTorque = verticalInput * motorForce / 2;
                break;
            default:
                break;
        }
        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    /// <summary>
    /// Applies the braking force to the wheels.
    /// </summary>
    private void ApplyBreaking()
    {
        leftFrontCollider.brakeTorque = currentBreakForce;
        leftRearCollider.brakeTorque = currentBreakForce;
        rightFrontCollider.brakeTorque = currentBreakForce;
        rightRearCollider.brakeTorque = currentBreakForce;
    }

    /// <summary>
    /// Handles the steering of the car.
    /// </summary>
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        leftFrontCollider.steerAngle = currentSteerAngle;
        rightFrontCollider.steerAngle = currentSteerAngle;
    }

    /// <summary>
    /// Updates the position and rotation of a single wheel.
    /// </summary>
    /// <param name="wheelColl">The wheel collider.</param>
    /// <param name="wheelTransform">The wheel transform.</param>
    private void UpdateWheels()
    {
        UpdateSingleWheel(leftFrontCollider, leftFrontTransform);
        UpdateSingleWheel(leftRearCollider, leftRearTransform);
        UpdateSingleWheel(rightFrontCollider, rightFrontTransform);
        UpdateSingleWheel(rightRearCollider, rightRearTransform);
    }

    /// <summary>
    /// Updates the wheel positions and rotations.
    /// </summary>
    /// <param name="wheelColl">The wheel collider.</param>
    /// <param name="wheelTransform">The wheel transform.</param>
    private void UpdateSingleWheel(WheelCollider wheelColl, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelColl.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    /// <summary>
    /// Updates the speedometer with the current speed.
    /// </summary>
    private void UpdateSpeedometer()
    {
        if (speedoMeterManager != null)
        {
            speedoMeterManager.vehicleSpeed = currentSpeed; // convert m/s to km/h
        }
    }

    /// <summary>
    /// Stops the car and its sounds.
    /// </summary>
    public void StopCar()
    {
        // Stop the audio
        if (motorAudio != null)
        {
            motorAudio.Stop();
        }
        // Optionally, you can also stop the test sound
        if (testSound != null)
        {
            testSound.Stop();
        }
        // Disable the car controller script
        Debug.Log("CarController: StopCar - Disabling CarController");
        isEnabled = false;
    }
}
