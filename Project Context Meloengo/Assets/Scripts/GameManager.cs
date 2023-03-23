using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private static GameManager _instance;

    public CharacterController playerController;
    public PlayerMovement playerMovement;
    public PlayerFollowing playerFollowers;
    public PlayerInteraction playerInteraction;

    public ConvinceMode convinceMode;
    public PersonSpawner personSpawner;
    public EnergyManager energyManager;
    public DieManager dieManager;
    public CorpBuilding corpBuilding;
    public TimeManager timeManager;
    public TalkWithPerson talkWithPerson;

    public Person currentPerson;

    public ConvoMode convoMode;

    private void Start()
    {
        ResetDay();
    }

    public void ContinueConversation()
    {
        talkWithPerson.gameObject.SetActive(true);
        talkWithPerson.StartConversation();
    }

    public void StartConversation(Person person)
    {
        if (energyManager.currentEnergy > 0)
        {
            playerInteraction.gameObject.SetActive(false);
            currentPerson = person;
            talkWithPerson.gameObject.SetActive(true);
            convoMode = ConvoMode.PREBATTLE;
            talkWithPerson.currentDialog = currentPerson.preBattleDialog[Random.Range(0, currentPerson.preBattleDialog.Count)];
            talkWithPerson.StartConversation();
        }
    }

    public void StartConvinceMode()
    {
        talkWithPerson.gameObject.SetActive(false);
        convinceMode.StartConvinceMode(currentPerson);
        playerInteraction.gameObject.SetActive(true);
    }

    public void TakeOverCorpBuilding()
    {
        if (energyManager.currentEnergy > 0)
        {
            int amount = 0;
            amount += Random.Range(0, 6) + 1; //playerDie
            dieManager.ThrowDie(6);

            foreach (Follower followerDie in playerFollowers.GetAllFollowers())
            {
                amount += Random.Range(0, followerDie.GetDieValue()) + 1;
                dieManager.ThrowDie(followerDie.GetDieValue());
            }

            if (amount >= corpBuilding.convinceAmount)
            {
                Destroy(corpBuilding);
            }
            else
            {
                corpBuilding.interactable = false;
            }
            energyManager.DecreaseEnergy();
        }
    }

    public void ResetDay()
    {
        personSpawner.SpawnNewPersons();
        energyManager.ResetEnergy();
        if (corpBuilding != null)
        {
            corpBuilding.interactable = true;
        }
        timeManager.ResetDay();
    }
}

public enum ConvoMode
{
    PREBATTLE = 0,
    PREPREBATTLE,
    INBATTLE,
    POSTBATTLE,
}