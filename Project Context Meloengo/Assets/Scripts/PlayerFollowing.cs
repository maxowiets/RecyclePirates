using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowing : MonoBehaviour
{
    public Follower follower;
    List<Follower> followers = new List<Follower>();
    public float followerDistance = 1f;
    public int formationWidth = 5;

    Vector3 lastMoveDirection;

    Vector3 previousPosition;

    private void Start()
    {
        lastMoveDirection = transform.forward;
    }

    private void Update()
    {
        //set heading direction
        if ((transform.position - previousPosition).magnitude > 0)
        {
            lastMoveDirection = (transform.position - previousPosition).normalized * followerDistance;
            previousPosition = transform.position;
        }

        //set destination of followers
        int remainingFollowers = followers.Count;
        int currentRow = 1;
        int currentFollower = 0;
        while (remainingFollowers > 0)
        {
            Vector3 startingOffset;
            if (remainingFollowers >= formationWidth)
            {
                startingOffset = Quaternion.Euler(0, -90, 0) * (lastMoveDirection * (formationWidth - 1) * 0.5f) - lastMoveDirection * currentRow;
                for (int i = 0; i < formationWidth; i++)
                {
                    followers[currentFollower].SetDestination(transform.position + startingOffset + Quaternion.Euler(0, 90, 0) * (lastMoveDirection * i));
                    currentFollower++;
                }
            }
            else
            {
                startingOffset = Quaternion.Euler(0, -90, 0) * (lastMoveDirection * ((remainingFollowers % formationWidth - 1) * 0.5f)) - lastMoveDirection * currentRow;
                for (int i = 0; i < remainingFollowers % formationWidth; i++)
                {
                    followers[currentFollower].SetDestination(transform.position + startingOffset + Quaternion.Euler(0, 90, 0) * (lastMoveDirection * i));
                    currentFollower++;
                }
            }
            
            remainingFollowers -= formationWidth;
            currentRow++;
        }
    }

    public void AddToFollowerList(int dieValue)
    {
        var newFollower = Instantiate(follower, transform.position, Quaternion.identity);
        newFollower.GetComponent<Follower>().SetDieValue(dieValue);
        followers.Add(newFollower);
    }

    public List<Follower> GetAllFollowers()
    {
        return followers;
    }
}
