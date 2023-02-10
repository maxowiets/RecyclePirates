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

    private void Start()
    {
        tmpValue.text = convinceAmount.ToString();
        tmpValue.gameObject.SetActive(false);
    }

    public void ShowStats()
    {
        tmpValue.gameObject.SetActive(true);
    }

    public void HideStats()
    {
        tmpValue.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        HideStats();
    }
}
