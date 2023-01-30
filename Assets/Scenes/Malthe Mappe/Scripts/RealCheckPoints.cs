using UnityEngine;

public class RealCheckPoints : MonoBehaviour
{
    private static Vector3 spawnPoint;
    private static Vector3 Offset;
    private bool playerTriggered = false;
    public Transform Player;
    
    public Quaternion originalRotationValue;
    CarController car;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnPoint = transform.position;
            playerTriggered = true;
        }
    }


    private void Update()
    {
        if (playerTriggered && Input.GetKey("r"))
        {
            Respawn();
        }
    }
    public void Respawn()
    {

        Player.position = spawnPoint;
        Player.rotation = originalRotationValue;
        car.frontLeftWheelCollider.motorTorque = 0f;
        car.frontRightWheelCollider.motorTorque = 0f;



    }
}
