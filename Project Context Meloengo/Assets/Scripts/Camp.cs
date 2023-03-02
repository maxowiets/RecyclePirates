using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public List<Transform> meetingPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            GameManager.Instance.playerFollowers.EnterCamp(meetingPoints);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            GameManager.Instance.playerFollowers.ExitCamp();
        }
    }
}
