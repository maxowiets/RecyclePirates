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
    FollowMode followMode;

    private void Start()
    {
        lastMoveDirection = transform.forward;
        followMode = FollowMode.FOLLOWPLAYER;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddToFollowerList();
        }

        switch (followMode)
        {
            case FollowMode.FOLLOWPLAYER:
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
                break;
            case FollowMode.CONVINCE:
                break;
            case FollowMode.CAMP:
                break;
            default:
                break;
        }
    }

    public void AddToFollowerList()
    {
        int rand = Random.Range(0, 7);

        switch (rand)
        {
            case 0:
                AddToFollowerList(2);
                break;
            case 1:
                AddToFollowerList(4);
                break;
            case 2:
                AddToFollowerList(6);
                break;
            case 3:
                AddToFollowerList(8);
                break;
            case 4:
                AddToFollowerList(10);
                break;
            case 5:
                AddToFollowerList(12);
                break;
            case 6:
                AddToFollowerList(20);
                break;
            default:
                break;
        }
    }

    public void AddToFollowerList(int dieValue)
    {
        var newFollower = Instantiate(follower, transform.position, Quaternion.identity);
        newFollower.GetComponent<Follower>().SetDieValue(dieValue);
        followers.Add(newFollower);
    }

    public void EnableConvinceMode()
    {
        followMode = FollowMode.CONVINCE;

        Vector3 convinceModeOffset = new Vector3(0.7f * followerDistance, 0, 0.5f * followerDistance);

        //set destination of followers
        int remainingFollowers = followers.Count;
        int currentRow = 1;
        int currentFollower = 0;
        while (remainingFollowers > 0)
        {
            Vector3 startingOffset;
            if (remainingFollowers >= formationWidth)
            {
                startingOffset = Quaternion.Euler(0, -90, 0) * (convinceModeOffset * (formationWidth - 1) * 0.5f) - convinceModeOffset * currentRow;
                for (int i = 0; i < formationWidth; i++)
                {
                    followers[currentFollower].transform.position = (transform.position + startingOffset + Quaternion.Euler(0, 90, 0) * (convinceModeOffset * i) + followers[currentFollower].GetDestinationOffset());
                    currentFollower++;
                }
            }
            else
            {
                startingOffset = Quaternion.Euler(0, -90, 0) * (convinceModeOffset * ((remainingFollowers % formationWidth - 1) * 0.5f)) - convinceModeOffset * currentRow;
                for (int i = 0; i < remainingFollowers % formationWidth; i++)
                {
                    followers[currentFollower].transform.position = (transform.position + startingOffset + Quaternion.Euler(0, 90, 0) * (convinceModeOffset * i) + followers[currentFollower].GetDestinationOffset());
                    currentFollower++;
                }
            }

            remainingFollowers -= formationWidth;
            currentRow++;
        }
        foreach (Follower follower in followers)
        {
            follower.enabled = false;
            follower.DisableWalking();
        }
    }

    public void DisableConvinceMode()
    {
        followMode = FollowMode.FOLLOWPLAYER;
        foreach (Follower follower in followers)
        {
            follower.transform.position = transform.position;
            follower.enabled = true;
        }
    }

    public void EnterCamp(List<Transform> meetingPoints)
    {
        followMode = FollowMode.CAMP;
        foreach (Follower follower in followers)
        {
            Vector2 randOffset = Random.insideUnitCircle * 3f;
            Vector3 destinationOffset = new Vector3(randOffset.x, 0, randOffset.y);

            follower.SetDestination(meetingPoints[Random.Range(0, meetingPoints.Count)].position + destinationOffset);
        }
    }

    public void ExitCamp()
    {
        followMode = FollowMode.FOLLOWPLAYER;
    }

    public List<Follower> GetAllFollowers()
    {
        return followers;
    }
}

public enum FollowMode
{
    FOLLOWPLAYER = 0,
    CONVINCE,
    CAMP,
}