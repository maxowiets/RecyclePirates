using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int convinceAmount;
    public int dieValue;

    public TextMeshPro tmpValue;

    public bool interactable = true;

    public GameObject cloud;

    private void Start()
    {
        tmpValue.text = convinceAmount.ToString();
        tmpValue.gameObject.SetActive(false);
    }

    public void ShowStats()
    {
        if (interactable) tmpValue.gameObject.SetActive(true);
    }

    public void HideStats()
    {
        tmpValue.gameObject.SetActive(false);
    }

    public void FailInteraction()
    {
        interactable = false;
        cloud.SetActive(true);
    }

    private void OnDestroy()
    {
        HideStats();
    }
}
