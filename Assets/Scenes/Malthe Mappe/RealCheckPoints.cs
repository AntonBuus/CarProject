using UnityEngine;

public class RealCheckPoints : MonoBehaviour
{
    private static Vector3 spawnPoint;
    private bool playerTriggered = false;
    public Transform Player;

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

    }
}
