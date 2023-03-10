using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public int maxEnergy;
    public int currentEnergy;

    public Image energySprite;

    int width;
    int height;

    private void Awake()
    {
        width = (int)energySprite.GetComponent<RectTransform>().sizeDelta.x;
        height = (int)energySprite.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }

    public void UseEnergy()
    {
        currentEnergy--;
        UpdateUI();
    }

    public void IncreaseEnergy()
    {
        currentEnergy++;
        UpdateUI();
    }

    void UpdateUI()
    {
        energySprite.GetComponent<RectTransform>().sizeDelta = new Vector2(currentEnergy * width, height);
    }
}
