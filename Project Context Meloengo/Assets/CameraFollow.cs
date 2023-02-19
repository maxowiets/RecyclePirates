using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothing;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, smoothing * Time.deltaTime);
    }
}
