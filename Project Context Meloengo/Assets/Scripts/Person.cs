using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int convinceAmount;
    public int dieValue;

    public TextMeshPro tmpValue;

    private void Start()
    {
        tmpValue.text = convinceAmount.ToString();
        tmpValue.gameObject.SetActive(false);
    }

    public void ShowStats()
    {
        Debug.Log("Convince Amount = " + convinceAmount);
        tmpValue.gameObject.SetActive(true);
    }

    public void HideStats()
    {
        Debug.Log("Stats Hidden");
        tmpValue.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        HideStats();
    }
}
