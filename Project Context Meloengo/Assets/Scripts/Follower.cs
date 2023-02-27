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
    FollowerCard card;

    private void Start()
    {
        movespeed = Random.Range(4, 8);
        anim = GetComponentInChildren<Animator>();
        anim.speed = movespeed / 6f;
        Vector2 randOffset = Random.insideUnitCircle * 0.2f;
        destinationOffset = new Vector3(randOffset.x, 0, randOffset.y);
        card = GetComponentInChildren<FollowerCard>();
        card.SetDieValue(dieValue);
        card.gameObject.SetActive(false);
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
        card?.SetDieValue(dieValue);
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDestinationOffset()
    {
        return destinationOffset;
    }

    public void DisableWalking()
    {
        anim.SetBool("Walking", false);
    }

    public void OnHoverEnter()
    {
        card.gameObject.SetActive(true);
    }

    public void OnHoverExit()
    {
        card.gameObject.SetActive(false);
    }
}