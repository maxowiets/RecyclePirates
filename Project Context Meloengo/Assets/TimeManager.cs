using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 180;
    float maxAngleSun = 120;
    float currentTime;
    int currentDay;
    public Light sunLight;
    Quaternion startTransformSun;
    float rotationSpeed;
    float timeSpeedMultiplier = 1;

    public AnimationCurve shadowCurve;

    // Start is called before the first frame update
    void Start()
    {
        currentDay = 0;
        startTransformSun = sunLight.transform.rotation;
        rotationSpeed = maxAngleSun / dayDuration;
    }

    // Update is called once per frame
    void Update()
    {
        sunLight.transform.RotateAround(sunLight.transform.position, Vector3.forward, rotationSpeed * timeSpeedMultiplier * Time.deltaTime);
        sunLight.shadowStrength = shadowCurve.Evaluate(currentTime / dayDuration);
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
        if (startTransformSun.y != 0)
        {
            sunLight.transform.rotation = startTransformSun;
        }
        currentDay++;
    }

    public float GetRemainingTime()
    {
        return dayDuration - currentTime;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }
}
