using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHoverFollower : MonoBehaviour
{
    public LayerMask personLayer;
    Follower currentFollower = null;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,100, personLayer))
        {
            if (currentFollower == null || currentFollower != hit.transform.GetComponent<Follower>())
            {
                if (currentFollower != null)
                {
                    currentFollower.OnHoverExit();
                }
                currentFollower = hit.transform.GetComponent<Follower>();
                currentFollower.OnHoverEnter();
            }
        }
        else
        {
            if (currentFollower != null)
            {
                currentFollower.OnHoverExit();
                currentFollower=null;
            }
        }

        if (currentFollower != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentFollower.SelectFollower();
            }
        }
    }
}
