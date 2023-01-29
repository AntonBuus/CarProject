using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float maxReverseSpeed = -50f;
    public float maxSteerAngle = 45f;
    public float motorForce = 50f;
    public float brakeForce = 100f;
    public float handbrakeForce = 150f;
    public float downForce = 100f;
    public float suspensionHeight = 0.2f;
    public float suspensionDamper = 50f;
    public float suspensionSpring = 35f;
    public Transform[] wheelMesh;
    public Transform centerOfMass;
    public WheelCollider[] wheelCollider;
    public bool handbrake;
    public bool autoReverse;

    private float currentSpeed;
    private float currentSteerAngle;
    private float currentBrake;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        currentSpeed = 0f;
        currentSteerAngle = 0f;
        currentBrake = 0f;
    }

    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        handbrake = Input.GetKey(KeyCode.X);

        // Acceleration
        if (moveVertical > 0f)
        {
            currentSpeed += motorForce * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxSpeed);
        }
        else if (moveVertical < 0f)
        {
            if (!autoReverse)
            {
                currentSpeed -= brakeForce * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, 0f);
            }
            else
            {
                currentSpeed -= motorForce * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxSpeed);
            }
        }
        else
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= brakeForce * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
            }
            else if (currentSpeed < 0f)
            {
                currentSpeed += brakeForce * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, 0f);
            }
        }

        // Steering
        currentSteerAngle = moveHorizontal * maxSteerAngle;

        // Braking
        if (handbrake)
        {
            currentBrake = handbrakeForce;
        }
        else
        {
            currentBrake = 0f;
        }  
      
       // Apply forces to the car
    rb.AddForce(transform.forward * currentSpeed);
    rb.AddRelativeTorque(0f, currentSteerAngle, 0f);
    for (int i = 0; i < wheelCollider.Length; i++)
    {
        wheelCollider[i].steerAngle = currentSteerAngle;
        wheelCollider[i].brakeTorque = currentBrake;
        wheelCollider[i].motorTorque = currentSpeed;

        // Suspension
        // WheelHit hit;
        // if (wheelCollider[i].GetGroundHit(out hit))
        // {
        //     float travel = (-wheelCollider[i].transform.InverseTransformPoint(hit.point).y - wheelCollider[i].radius) / wheelCollider[i].suspensionDistance;
        //     Vector3 force = (suspensionHeight - travel) * suspensionSpring;
        //     rb.AddForceAtPosition(wheelCollider[i].transform.up * force, wheelCollider[i].transform.position);
        // }

        // Wheel Mesh Rotation
        wheelMesh[i].rotation = wheelCollider[i].transform.rotation * Quaternion.Euler(0f, wheelCollider[i].steerAngle, 90f);
    }

    // Downforce
    rb.AddForce(-transform.up * downForce);
    }
}

