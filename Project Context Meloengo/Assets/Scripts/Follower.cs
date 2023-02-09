using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Follower(int _dieValue)
    {
        dieValue = _dieValue;
    }

    float movespeed = 12f;
    int dieValue;

    Vector3 destination;

    private void Start()
    {
        movespeed = Random.Range(4, 8);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, movespeed * Time.deltaTime);
    }

    public int GetDieValue()
    {
        return dieValue;
    }

    public void SetDieValue(int newDieValue)
    {
        dieValue = newDieValue;
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
}
