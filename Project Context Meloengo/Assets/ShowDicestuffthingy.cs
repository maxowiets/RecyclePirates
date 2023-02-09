using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowDicestuffthingy : MonoBehaviour
{
    public GameObject counterText;
    public GameObject counterText1;
    public GameObject counterText2;
    public GameObject counterText3;
    public GameObject counterText4;
    public GameObject counterText5;
    public GameObject counterText6;
    public GameObject counterText7;
    public OnDiceCalculations dice;
    public OnDiceCalculations dice1;
    public OnDiceCalculations dice2;
    public OnDiceCalculations dice3;
    public OnDiceCalculations dice4;
    public OnDiceCalculations dice5;
    public OnDiceCalculations dice6;
    public DiceCalculationManager Man;

    public void Update()
    {
        counterText.GetComponent<TMPro.TextMeshProUGUI>().text = dice.individualDiceScore.ToString();
        counterText1.GetComponent<TMPro.TextMeshProUGUI>().text = dice2.individualDiceScore.ToString();
        counterText2.GetComponent<TMPro.TextMeshProUGUI>().text = dice3.individualDiceScore.ToString();
        counterText3.GetComponent<TMPro.TextMeshProUGUI>().text = dice4.individualDiceScore.ToString();
        counterText4.GetComponent<TMPro.TextMeshProUGUI>().text = dice5.individualDiceScore.ToString();
        counterText5.GetComponent<TMPro.TextMeshProUGUI>().text = dice6.individualDiceScore.ToString();
        counterText6.GetComponent<TMPro.TextMeshProUGUI>().text = dice1.individualDiceScore.ToString();
        counterText7.GetComponent<TMPro.TextMeshProUGUI>().text = Man.totalDiceScore.ToString();
    }
}