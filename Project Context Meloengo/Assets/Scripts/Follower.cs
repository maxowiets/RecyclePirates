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
    Vector3 destinationOffset;
    Animator anim;

    private void Start()
    {
        movespeed = Random.Range(4, 8);
        anim = GetComponentInChildren<Animator>();
        anim.speed = movespeed / 6f;
        Vector2 randOffset = Random.insideUnitCircle * 0.3f;
        destinationOffset = new Vector3(randOffset.x, 0, randOffset.y);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination + destinationOffset) < 0.01f)
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
        }
        transform.position = Vector3.MoveTowards(transform.position, destination + destinationOffset, movespeed * Time.deltaTime);
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
