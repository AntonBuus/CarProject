using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

   
    private float Horizontal;
    private float Vertical;

    public bool ready = false;

    AudioManager Audio;

    public float downTime, upTime, pressTime = 0;
    public float countDown = 2.0f;
    public Material GlowingMat;
    private float horizontalInput;
    private float verticalInput;
    private float currentbreakForce;
    private float nobreakingForce = 0f;
    private float steerAngle;
    private float currentSteerAngle;
    public bool isBreaking;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;
    



    public Rigidbody rb; 
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;


    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;



    private void Awake()
    {
        GlowingMat.DisableKeyword("_EMISSION");
        
    }


    private void Start()
    {
        leftParticle.Stop();
        rightParticle.Stop();
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        

        

        if (Input.GetKey(KeyCode.Space))
        {
            isBreaking = true;
            GlowingMat.EnableKeyword("_EMISSION");
        }
        else
        {
            isBreaking = false;
            GlowingMat.DisableKeyword("_EMISSION");
        }

    }

    private void GetInput()
    {


        {
            if (Input.GetKey(KeyCode.D) && ready == false)
            {
                downTime = Time.time;
                pressTime = downTime + countDown;
                ready = true;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                ready = false;
            }
            if (Time.time >= pressTime && ready == true)
            {
                
                
                leftParticle.Play();
                rightParticle.Play();
                //Debug.Log("Hello");

                
            }
            else
            {
                leftParticle.Stop();
                rightParticle.Stop();
            }

            
        }





        if (Input.GetKey(KeyCode.D))
        {
            Horizontal = 1f;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            Horizontal = -1f;
        }
        else
        {
            Horizontal = 0f;
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            Vertical = 1f;
            FindObjectOfType<AudioManager>().Play("CarDriving");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vertical = -1f;
        }
        else
        {
            Vertical = 0f;
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = Vertical * motorForce; //Den Tager det individuelle hjul og drejer det
        frontRightWheelCollider.motorTorque = Vertical * motorForce; // Tilf�j forhjulene ogs� bliver taget med
        

        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
            

        }
        else if (!isBreaking) 
        {
            
            frontRightWheelCollider.brakeTorque = nobreakingForce;
            frontLeftWheelCollider.brakeTorque = nobreakingForce;
            rearLeftWheelCollider.brakeTorque = nobreakingForce;
            rearRightWheelCollider.brakeTorque = nobreakingForce; //nul breaking force tilf�jes her, variablen er skrevet over

        }
        

        
    }

    private void ApplyBreaking() // Den stopper ikke n�r man ikke trykker
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;


    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * Horizontal;
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
