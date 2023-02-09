using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCalculationManager : MonoBehaviour
{
    [Header("DiceScore")]
    public int totalDiceScore;

    [Header("PassiveInformation")]
    public AbilityActivator abilityActivate;
    private int passiveOrder;
    public float timeToWaitBetweenPassives;

    [HideInInspector]
    public GameObject[] diceArray;

    private GameObject tempGO;
    void Start()
    {
        FillArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(InitiatePassive());
        }
    }

    public void Spawndie()
    {

    }

    public void FillArray()
    {
        diceArray = GameObject.FindGameObjectsWithTag("Dice");
        ShuffleArray();
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i].GetComponent<OnDiceCalculations>().positionInArrayOrder = i;
        }
    }

    public void ShuffleArray()
    {
        for (int i = 0; i < diceArray.Length; i++)
        {
            int rnd = Random.Range(0, diceArray.Length);
            tempGO = diceArray[rnd];
            diceArray[rnd] = diceArray[i];
            diceArray[i] = tempGO;
        }
    }

    public void CalculateDiceScore()
    {
        totalDiceScore = 0;

        for (int i = 0; i < diceArray.Length; i++)
        {
            totalDiceScore = totalDiceScore + diceArray[i].GetComponent<OnDiceCalculations>().individualDiceScore;
        }

        Debug.Log("The Total Dice Score is " + totalDiceScore.ToString());
    }

    public IEnumerator InitiatePassive()
    {
        passiveOrder = 0;

        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(timeToWaitBetweenPassives);
            for (int p = 0; p < diceArray.Length; p++)
            {
                diceArray[p].GetComponent<OnDiceCalculations>().PassiveExecution(passiveOrder);
                Debug.Log("PassiveActivated");
                CalculateDiceScore();
            }
            passiveOrder++;
            Debug.Log("Passive order " + passiveOrder + " Has Been Executed");
        }

        for (int i = 0; i < diceArray.Length; i++)
        {
            diceArray[i].GetComponent<OnDiceCalculations>().PassiveExecution(passiveOrder);
        }

        CalculateDiceScore();
    }

    public void AbilityHandler(int AbiltyType, int orderInArray)
    {
        string abilityNumber = "Ability" + AbiltyType.ToString();
        gameObject.SendMessage(abilityNumber, orderInArray);
    }

    public void ActivatedHandler(int AbiltyType, int orderInArray)
    {
        string abilityNumber = "ActivatedAbility" + AbiltyType.ToString();
        gameObject.SendMessage(abilityNumber, orderInArray);
    }

    public void AddScoreToDice(int scoreToAdd, int orderOfDice)
    {
        diceArray[orderOfDice].GetComponent<OnDiceCalculations>().individualDiceScore = diceArray[orderOfDice].GetComponent<OnDiceCalculations>().individualDiceScore + scoreToAdd;
    }

    public int GetScore(int orderOfDice)
    {
        int Score = 0;
        if (orderOfDice == -100)
        {
            Score = totalDiceScore;
        }
        Score = Score + diceArray[orderOfDice].GetComponent<OnDiceCalculations>().individualDiceScore;
        return Score;
    }
}
