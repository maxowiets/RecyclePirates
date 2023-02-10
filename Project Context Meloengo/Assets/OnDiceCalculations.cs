using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDiceCalculations : MonoBehaviour
{
    private DiceCalculationManager diceManager;

    [SerializeField]
    private DiceBlueprint diceValues;

    [Header("Score")]
    public int individualDiceScore;

    [Header("Stat boosts")]
    public int minimumRollStatBoost;
    public int maximumRollStatBoost;

    [Header("position")]
    public int positionInArrayOrder;
    public int xPos;
    public int yPos;
    private void Awake()
    {
        diceManager = FindObjectOfType<DiceCalculationManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Active();
        }
    }

    private void FirstRoll()
    {
        individualDiceScore = diceValues.OriginalDiceRoll(minimumRollStatBoost, maximumRollStatBoost);
        Debug.Log(diceValues.nameOfDie + " Rolled A " + individualDiceScore.ToString());
    }

    public void PassiveExecution(int orderExecuting)
    {
        if (orderExecuting == 0)
        {
            FirstRoll();
        }
        if (!diceValues.passive) return;

        if(orderExecuting == diceValues.passiveOrder)
        {
            Passive();
        }
    }

    private void Active()
    {
        diceManager.ActivatedHandler(diceValues.activatedType, positionInArrayOrder);
    }

    private void Passive()
    {
        diceManager.AbilityHandler(diceValues.abilityType, positionInArrayOrder);
    }


}
