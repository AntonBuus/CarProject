using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZoom : MonoBehaviour
{   
    public CameraAttach _CamAt;
    public bool speedbool;

    public void OnTriggerExit(Collider collision) 
    {
        if (collision.gameObject.tag == "Speed_Boost")
        {
            _CamAt.SpeedBoostZoom();
        }
    }
}
