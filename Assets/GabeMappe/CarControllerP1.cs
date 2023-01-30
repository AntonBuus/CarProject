using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerP1 : MonoBehaviour
{

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";


    private float horizontalInput;
    private float verticalInput;
    private float currentbreakForce;
    private float nobreakingForce = 0f;
    private float steerAngle;
    private float currentSteerAngle;
    public bool isBreaking;


    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;
    Vector3 movement;



    public Rigidbody rb; 
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;


    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;



  

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        

        if (Input.GetKey(KeyCode.Space))
        {
            isBreaking = true;
        }
        else
        {
            isBreaking = false;
        }

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
       

       
        

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce; //Den Tager det individuelle hjul og drejer det
        frontRightWheelCollider.motorTorque = verticalInput * motorForce; // Tilføj forhjulene også bliver taget med
        

        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
            Debug.Log("i am breaking");

        }
        else if (!isBreaking) 
        {
            Debug.Log("i am not breaking");
            frontRightWheelCollider.brakeTorque = nobreakingForce;
            frontLeftWheelCollider.brakeTorque = nobreakingForce;
            rearLeftWheelCollider.brakeTorque = nobreakingForce;
            rearRightWheelCollider.brakeTorque = nobreakingForce; //nul breaking force tilføjes her, variablen er skrevet over

        }
        

        
    }

    private void ApplyBreaking() // Den stopper ikke når man ikke trykker
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;


    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;

    }



    

    private void UpdateWheels() //Updating the visuals of the wheel
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);




    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }
}
