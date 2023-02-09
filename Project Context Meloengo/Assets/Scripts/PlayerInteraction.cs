using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Person>())
        {
            other.GetComponent<Person>().ShowStats();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Person>())
        {
            if (Input.GetKeyDown(interactKey))
            {
                Debug.Log("test");
                GameManager.Instance.StartConvinceMode(other.GetComponent<Person>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Person>())
        {
            other.GetComponent<Person>().HideStats();
        }
    }
}
