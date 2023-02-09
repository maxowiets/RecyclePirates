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
            foreach (Follower followerDie in playerFollowers.GetAllFollowers())
            {
                amount += Random.Range(0, followerDie.GetDieValue()) + 1;
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
                person.enabled = false;
            }
            energyManager.UseEnergy();
        }
    }

    public void ResetDay()
    {
        personSpawner.SpawnNewPersons();
        energyManager.ResetEnergy();
    }


}