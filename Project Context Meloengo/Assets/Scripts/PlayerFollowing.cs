using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowing : MonoBehaviour
{
    public Follower follower;
    List<Follower> followers = new List<Follower>();

    public void AddToFollowerList(int dieValue)
    {
        Instantiate(follower);
        //followers.Add();
    }

    public List<Follower> GetAllFollowers()
    {
        return followers;
    }
}
