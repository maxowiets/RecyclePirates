using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDayTrigger : MonoBehaviour
{
    public Button nextDayButton;

    private void Start()
    {
        nextDayButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            nextDayButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.GetComponent<CharacterController>())
        //{

        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            nextDayButton.gameObject.SetActive(false);
        }
    }
}
