using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

   
    private float Horizontal;
    private float Vertical;

    public bool ready = false;
    private bool guf = true;
    private float accel = 1;
    //AudioManager Audio;

    public float downTime, upTime, pressTime = 0;
    public float countDown = 4.0f;
    public Material GlowingMat;
    private float horizontalInput;
    private float verticalInput;
    private float currentbreakForce;
    private float nobreakingForce = 0f;
    private float steerAngle;
    private float currentSteerAngle;
    public bool isBreaking;
    public GameObject leftParticle;
    public GameObject rightParticle;
    public GameObject rightSparkParticle;


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

    private void Update()
    {
       /* 
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                downTime = Time.time;
                Debug.Log(downTime);
                
                if (countDown <= downTime)
                {
                    if (guf == true)
                    {
                        leftParticle.SetActive(true);
                        rightParticle.SetActive(true);
                        guf = false;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                
                guf = true;
                downTime = downTime - Time.time;
                pressTime = 0f;
                downTime = 0f;
                leftParticle.SetActive(false);
                rightParticle.SetActive(false);
            }
            else
            {
                
            }
           
        }

        */

        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                
                    if (guf == true)
                    {
                        StartCoroutine("SetActive");
                        
                        guf = false;
                    }
                
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {

                guf = true;
                
                leftParticle.SetActive(false);
                rightParticle.SetActive(false);
                rightSparkParticle.SetActive(false);
            }
            else
            {
                leftParticle.SetActive(false);
                rightParticle.SetActive(false);
                rightSparkParticle.SetActive(false);
            }
            
        }




    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(2);
        leftParticle.SetActive(true);
        rightParticle.SetActive(true);
        guf = true;
        yield return new WaitForSeconds(2);
        rightSparkParticle.SetActive(true);


    }
    private void Start()
    {
        leftParticle.SetActive(false);
        rightParticle.SetActive(false);

        FindObjectOfType<AudioManager>().Play("SpitFire");

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
        if (Input.GetKey(KeyCode.W) && !isBreaking)
        {
            Vertical = 1f;
            //FindObjectOfType<AudioManager>().Play("CarDriving"); //dårlig ide med find objekt but i will have to do for now
            if (accel <= 10)
            {
                accel += + 0.1f; //Remember to change the mass of the wheels
            }
        }
        else if (Input.GetKey(KeyCode.S) && !isBreaking)
        {
            Vertical = -1f;
            if (accel <= 10)
            {
                accel += +0.1f; //Remember to change the mass of the wheels
            }
        }
        else
        {
            Vertical = 0f;
            accel = 0f; // den ganger konstant med det nedereste
        }
    }
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = Vertical * accel * motorForce; //Den Tager det individuelle hjul og drejer det
        frontRightWheelCollider.motorTorque = Vertical * accel * motorForce; // Tilføj forhjulene også bliver taget med
        Debug.Log(frontLeftWheelCollider.motorTorque);
       
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
            rearRightWheelCollider.brakeTorque = nobreakingForce; //nul breaking force tilføjes her, variablen er skrevet over

        }
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
