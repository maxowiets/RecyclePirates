using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowerCard : MonoBehaviour
{
    TextMeshPro dieAmount;
    // Start is called before the first frame update
    void Awake()
    {
        dieAmount = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDieValue(int value)
    {
        dieAmount.text = value.ToString();
    }
}
