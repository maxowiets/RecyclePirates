using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 180;
    float maxAngleSun = 120;
    float currentTime;
    public Transform sun;
    Quaternion startTransformSun;
    float rotationSpeed;
    float timeSpeedMultiplier = 1;


    // Start is called before the first frame update
    void Start()
    {
        startTransformSun = sun.rotation;
        rotationSpeed = maxAngleSun / dayDuration;
    }

    // Update is called once per frame
    void Update()
    {
        sun.RotateAround(sun.position, Vector3.forward, rotationSpeed * timeSpeedMultiplier * Time.deltaTime);
        currentTime += timeSpeedMultiplier * Time.deltaTime;
        if (currentTime >= dayDuration)
        {
            GameManager.Instance.ResetDay();
        }
    }

    public void SetRotationSpeed(float newSpeed)
    {
        timeSpeedMultiplier = newSpeed;
    }

    public void ResetDay()
    {
        currentTime = 0;
        timeSpeedMultiplier = 1;
        sun.rotation = startTransformSun;
    }

    public float GetRemainingTime()
    {
        return dayDuration - currentTime;
    }
}
