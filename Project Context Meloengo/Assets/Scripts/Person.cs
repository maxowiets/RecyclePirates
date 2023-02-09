using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int convinceAmount;
    public int dieValue;

    public void ShowStats()
    {
        Debug.Log("Convince Amount = " + convinceAmount);
    }

    public void HideStats()
    {
        Debug.Log("Stats Hidden");
    }
}
