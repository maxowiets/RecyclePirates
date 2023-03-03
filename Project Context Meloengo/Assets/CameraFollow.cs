using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothing;
    float offsetMultiplier = 1;
    float currentOffsetMultiplier = 1;
    float multiplierChangeSpeed = 1f;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
        offsetMultiplier = 1;
        currentOffsetMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset * currentOffsetMultiplier, smoothing * Time.deltaTime);

        currentOffsetMultiplier = Mathf.MoveTowards(currentOffsetMultiplier, offsetMultiplier, multiplierChangeSpeed * Time.deltaTime);
    }

    public void SetCameraOffsetMultiplier(float multiplier)
    {
        offsetMultiplier = multiplier;
    }
}
