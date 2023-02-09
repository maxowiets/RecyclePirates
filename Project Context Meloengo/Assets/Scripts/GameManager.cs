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

    public void StartConvinceMode(Person person)
    {
        int amount = 0;
        amount += Random.Range(0, 6) + 1; //playerDie
        foreach (Follower personDie in playerFollowers.GetAllFollowers())
        {
            amount += Random.Range(0, personDie.dieValue) + 1;
        }
        Debug.Log(amount);
        if (amount >= person.convinceAmount)
        {
            Debug.Log("Succesful");
            playerFollowers.AddToFollowerList(person.dieValue);
        }
    }
}