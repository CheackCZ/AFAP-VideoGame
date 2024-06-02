using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour
{
    private enum DrivenAxle { front, back, both };
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private GameObject mainCamera;

    [Space(30)]
    [Header("Konfigurace kamery")]
    public Vector3 cameraOffset;
    public float cameraTranslateSpeed;
    public float cameraRotationSpeed;

    [Space(30)]
    [Header("Nastavení auta")]
    [SerializeField] private DrivenAxle drivenAxle;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    private Rigidbody _rb;

    //[Space(30)]
    //[Header("Zvuky")]
    //[SerializeField] private AudioSource motorAudio;
    //[SerializeField] private float minimumPitch = 0.75f;
    //[SerializeField] private float maximumPitch = 2f;

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



    private float currentSpeed;
    private float currentPitch;

    //public AudioSource testSound;
    //public AudioClip testClip;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleCameraTranslation();
        HandleCameraRotation();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void Update()
    {
        //EngineSound();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //testSound.Play();
        }
        if(Input.GetKeyDown(KeyCode.Q)) {
            //AudioSource.PlayClipAtPoint(testClip, this.transform.position);
        }
    }


    //private void EngineSound()
    //{
    //    currentSpeed = Mathf.Abs(_rb.velocity.magnitude);
    //    currentPitch = Mathf.Abs(currentSpeed / 50f * verticalInput);

    //    if (currentSpeed < minSpeed)
    //    {
    //        motorAudio.pitch = minimumPitch;
    //    }
    //    if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
    //    {
    //        motorAudio.pitch = minimumPitch + currentPitch;
    //    }
    //    if (currentPitch > maxSpeed)
    //    {
    //        motorAudio.pitch = maximumPitch;
    //    }
    //}



    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
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
    private void ApplyBreaking()
    {
        leftFrontCollider.brakeTorque = currentBreakForce;
        leftRearCollider.brakeTorque = currentBreakForce;
        rightFrontCollider.brakeTorque = currentBreakForce;
        rightRearCollider.brakeTorque = currentBreakForce;
    }
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        leftFrontCollider.steerAngle = currentSteerAngle;
        rightFrontCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(leftFrontCollider, leftFrontTransform);
        UpdateSingleWheel(leftRearCollider, leftRearTransform);
        UpdateSingleWheel(rightFrontCollider, rightFrontTransform);
        UpdateSingleWheel(rightRearCollider, rightRearTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelColl, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelColl.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void HandleCameraTranslation()
    {
        var targetPosition = this.transform.TransformPoint(cameraOffset);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraTranslateSpeed * Time.deltaTime);
    }
    private void HandleCameraRotation()
    {
        var direction = this.transform.position - mainCamera.transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, rotation, cameraRotationSpeed * Time.deltaTime);
    }

}