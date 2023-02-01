using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttach : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    public Camera cam;

    // public SpeedZoom _SZ;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();

    }


    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, (Vector3.up));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        // noget med flip af kameraet hvis Z positionen er over
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        Countdown();
    }

    public void SpeedBoostZoom()
    {
        // if (_SZ.speedbool)
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 100f, 1f);
        // StartCoroutine("Waiter"); 
    }

    void Countdown()
    {
        while (cam.fieldOfView > 60)
        cam.fieldOfView -= 0.2f;
    }
}
