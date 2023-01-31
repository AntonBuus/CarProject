using System.Collections;
using UnityEngine;

public class RealCheckPoints : MonoBehaviour
{
    private static Vector3 spawnPoint;
    private static Vector3 Offset;
    private bool playerTriggered = false;
    public Transform Player;
    
    public Quaternion originalRotationValue;
    public CarController car;
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
            StartCoroutine("StopCar");
        }
    }
    public void Respawn()
    {

        Player.position = spawnPoint;
        Player.rotation = originalRotationValue;
        
       



    }
    
    IEnumerator StopCar()
    {
        yield return new WaitForEndOfFrame();
        car.rb.isKinematic = true;
        car.frontLeftWheelCollider.motorTorque = 0f;
        car.frontRightWheelCollider.motorTorque = 0f;
        yield return new WaitForSeconds(0.5f);
        car.rb.isKinematic = false;

    }
    
}
