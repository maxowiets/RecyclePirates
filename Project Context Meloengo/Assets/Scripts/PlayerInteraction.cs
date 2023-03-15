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
        else if (other.GetComponent<CorpBuilding>())
        {
            other.GetComponent<CorpBuilding>().ShowStats();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Person>())
        {
            if (other.GetComponent<Person>().interactable)
            {
                if (Input.GetKey(interactKey))
                {
                    GameManager.Instance.StartConversation(other.GetComponent<Person>());
                }
            }
        }
        else if (other.GetComponent<CorpBuilding>())
        {
            if (other.GetComponent<CorpBuilding>().interactable)
            {
                if (Input.GetKey(interactKey))
                {
                    GameManager.Instance.TakeOverCorpBuilding();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Person>())
        {
            other.GetComponent<Person>().HideStats();
        }
        else if (other.GetComponent<CorpBuilding>())
        {
            other.GetComponent<CorpBuilding>().HideStats();
        }
    }
}
