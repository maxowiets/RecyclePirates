using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetChanger : MonoBehaviour
{
    public float offsetMultiplier = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            Camera.main.GetComponent<CameraFollow>().SetCameraOffsetMultiplier(offsetMultiplier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            Camera.main.GetComponent<CameraFollow>().ResetCameraOffsetMultiplier();
        }
    }
}
