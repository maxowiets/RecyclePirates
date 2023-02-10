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

    public PlayerFollowing playerFollowers;
    public GameObject player;

    public PersonSpawner personSpawner;
    public EnergyManager energyManager;
    public DieManager dieManager;
    public CorpBuilding corpBuilding;

    private void Start()
    {
        ResetDay();
    }

    public void StartConvinceMode(Person person)
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

            Debug.Log(amount);
            if (amount >= person.convinceAmount)
            {
                Debug.Log("Succesful");
                playerFollowers.AddToFollowerList(person.dieValue);
                personSpawner.DestroyPerson(person);
            }
            else
            {
                person.interactable = false;
            }
            energyManager.UseEnergy();
        }
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
            energyManager.UseEnergy();
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
    }


}