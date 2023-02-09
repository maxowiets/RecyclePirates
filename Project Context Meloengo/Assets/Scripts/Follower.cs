using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Follower(int _dieValue)
    {
        dieValue = _dieValue;
    }

    public int dieValue;
}
