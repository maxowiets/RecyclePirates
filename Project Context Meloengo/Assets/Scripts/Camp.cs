using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public List<Transform> meetingPoints;
    public GameObject CampUI;
    public float ritualDurationPercentage = 10;
    public float ritualTimeSpeedUp = 2;
    bool ritualSucceeded = false;
    public Animator textAnim;

    PlayerMovement player;
    bool isRitualOn;

    private void Start()
    {
        player = GameManager.Instance.playerMovement;
        CampUI.SetActive(false);
    }

    private void Update()
    {
        if (isRitualOn)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position + Vector3.up, player.movespeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            GameManager.Instance.playerFollowers.EnterCamp(meetingPoints);
            CampUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            GameManager.Instance.playerFollowers.ExitCamp();
            CampUI.SetActive(false);
        }
    }

    public void StartRitual()
    {
        CampUI.SetActive(false);
        StartCoroutine(Ritual());
    }

    void EndRitual()
    {
        CampUI.SetActive(true);
        StopAllCoroutines();
        GameManager.Instance.timeManager.SetRotationSpeed(1f);
        GameManager.Instance.playerFollowers.EnterCamp(meetingPoints);

        //enable player movement
        GameManager.Instance.playerMovement.enabled = true;
        GameManager.Instance.playerController.enabled = true;
        isRitualOn = false;
        player.anim.SetBool("Walking", false);

        if (ritualSucceeded)
        {
            GameManager.Instance.energyManager.IncreaseEnergy();
        }

        Debug.Log("RITUAL ENDED");
    }

    IEnumerator Ritual()
    {
        ritualSucceeded = false;
        //calculate time needed for ritual
        float timeNeeded = GameManager.Instance.timeManager.dayDuration * ritualDurationPercentage * 0.01f;
        if (timeNeeded > GameManager.Instance.timeManager.GetRemainingTime())
        {
            textAnim.SetTrigger("Start");
            Debug.Log("Not Enough Time For Ritual");
            EndRitual();
            yield return 0;
            yield break;
        }

        //disable player movement
        GameManager.Instance.playerMovement.enabled = false;
        GameManager.Instance.playerController.enabled = false;
        isRitualOn = true;
        player.anim.SetBool("Walking", true);

        //set followers in circle position around the middle
        List<Follower> followers = GameManager.Instance.playerFollowers.GetAllFollowers();
        int followerAmount = followers.Count;
        float radius = Mathf.Sqrt((float)followerAmount * 30f) / (2 * Mathf.PI);

        for (int i = 0; i < followerAmount; i++)
        {
            followers[i].SetDestination(transform.position + Vector3.up + (Quaternion.Euler(0, (float)i/(float)followerAmount * 360f, 0) * Vector3.forward * radius));
        }
        yield return 0;
        Debug.Log("CHECK FOLLOWERS WALKING");
        bool allFollowersReady = false;
        while (allFollowersReady == false)
        {
            allFollowersReady = true;
            foreach (Follower follower in GameManager.Instance.playerFollowers.GetAllFollowers())
            {
                if (follower.GetCharacterAnimator().GetBool("Walking"))
                {
                    allFollowersReady = false;
                    Debug.Log("FOLLOWER WALKING");
                    break;
                }
            }
            Debug.Log("NOT READY");
            yield return 0;
        }

        Debug.Log("READY");

        //calculate time needed for ritual after walking
        timeNeeded = GameManager.Instance.timeManager.dayDuration * ritualDurationPercentage * 0.01f;
        if (timeNeeded > GameManager.Instance.timeManager.GetRemainingTime())
        {
            textAnim.SetTrigger("Start");
            Debug.Log("Not Enough Time For Ritual");
            EndRitual();
            yield return 0;
            yield break;
        }
        Invoke("EndRitual", timeNeeded / ritualTimeSpeedUp);
        GameManager.Instance.timeManager.SetRotationSpeed(ritualTimeSpeedUp);
        ritualSucceeded = true;
        yield return new WaitForSeconds(1f);

        //dance time
        int time = 0;
        while (true)
        {
            for (int i = 0; i < Mathf.CeilToInt(followerAmount / 7f); i++)
            {
                Invoke("ActivateSpeechBubble", Random.Range(0, 0.7f));
                if (Random.Range(0, 3) == 0)
                {
                    i++;
                }
            }
            time++;
            for (int i = 0; i < followerAmount; i++)
            {
                followers[i].SetDestination(transform.position + Vector3.up + (Quaternion.Euler(0, (float)i / (float)followerAmount * 360f + time * 30f, 0) * Vector3.forward * radius));
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void ActivateSpeechBubble()
    {
        GameManager.Instance.playerFollowers.GetAllFollowers()[Random.Range(0, GameManager.Instance.playerFollowers.GetAllFollowers().Count)].CampQuote();
    }
}
