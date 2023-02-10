using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityActivator : MonoBehaviour
{
    public DiceCalculationManager manager;

    public void Ability1(int orderInArray)
    {
        //Doubles Score
        manager.AddScoreToDice(manager.GetScore(orderInArray), orderInArray);
    }
    public void Ability2(int orderInArray)
    {
        //Adds score equal to the amount of dice total
        manager.AddScoreToDice(manager.diceArray.Length, orderInArray);
    }
    public void Ability3(int orderInArray)
    {
        //Adds score equal to the highest die
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Dice");
        GameObject tempGO;
        int highestScore = -999;
        for (int f = 0; f < tempArray.Length; f++)
        {
            int thisScore = tempArray[f].GetComponent<OnDiceCalculations>().individualDiceScore;
            if (thisScore > highestScore)
            {
                highestScore = thisScore;
                tempGO = tempArray[f];
            }
        }
        manager.AddScoreToDice(highestScore, orderInArray);
    }
    public void Ability4(int orderInArray)
    {
        //Adds score equal to the lowest die
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Dice");
        GameObject tempGO;
        int lowestScore = 1000000;
        for (int f = 0; f < tempArray.Length; f++)
        {
            int thisScore = tempArray[f].GetComponent<OnDiceCalculations>().individualDiceScore;
            if (thisScore < lowestScore)
            {
                lowestScore = thisScore;
                tempGO = tempArray[f];
            }
        }
        manager.AddScoreToDice(lowestScore, orderInArray);
    }
    public void Ability5(int orderInArray)
    {
        //if its odd it counts as 0 Otherwise its doubles
        if (manager.GetScore(orderInArray) % 2 == 1)
        {
            manager.AddScoreToDice(-manager.GetScore(orderInArray), orderInArray);
        }
        else
        {
            manager.AddScoreToDice(manager.GetScore(orderInArray), orderInArray);
        }
    }

    public void Ability6(int orderInArray)
    {
        //Doubles the highest Die
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Dice");
        GameObject tempGO = null;
        int highestScore = -999;
        for (int f = 0; f < tempArray.Length; f++)
        {
            int thisScore = tempArray[f].GetComponent<OnDiceCalculations>().individualDiceScore;
            if (thisScore > highestScore)
            {
                highestScore = thisScore;
                tempGO = tempArray[f];
            }
        }
        manager.AddScoreToDice(manager.GetScore(tempGO.GetComponent<OnDiceCalculations>().positionInArrayOrder), tempGO.GetComponent<OnDiceCalculations>().positionInArrayOrder);
    }

    public void Ability7(int orderInArray)
    {
        //Adds 2 to every Die
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Dice");
        for (int f = 0; f < tempArray.Length; f++)
        {
            int thisScore = tempArray[f].GetComponent<OnDiceCalculations>().individualDiceScore;
            manager.AddScoreToDice(2, tempArray[f]. GetComponent<OnDiceCalculations>().positionInArrayOrder);
        }
    }

    public void ActivatedAbility0(int orderInArray)
    {
        //There is nothing here
    }

    public void ActivatedAbility1(int orderInArray)
    {
        //Adds 2 score to the activator
        manager.AddScoreToDice(2, orderInArray);
    }
}
