using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceBlueprint")]
public class DiceBlueprint : ScriptableObject
{
    [Header("Name")]
    public string nameOfDie;

    [Header("Dice Variables")]
    public int minimumDice;
    public int maximumDice;
    public int statBoosts;

    [Header("Character Statistics")]
    public int statisticAnger;
    public int statisticFear;
    public int statisticHope;
    public int statisticImpotence;

    [Header("SpecialsInformation")]
    public bool passive;
    public bool active;
    [Range(1, 10)]
    public int passiveOrder;
    public int abilityType;
    public int activatedType;

    public int OriginalDiceRoll(int minimumStatBoost, int maximumStatBoost)
    {
        minimumDice = minimumDice + minimumStatBoost;
        maximumDice = maximumDice + maximumStatBoost;
        int rollOutcome = Random.Range(minimumDice, maximumDice + 1);
        return rollOutcome;
    }

}
