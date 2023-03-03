using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            Camera.main.GetComponent<CameraFollow>().SetCameraOffsetMultiplier(10f / Mathf.Abs(transform.position.z - other.transform.position.z));
        }
    }
}
