using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvinceMode : MonoBehaviour
{
    bool convincing = false;

    int dieAmount = 0;

    Person currentPerson;
    bool newPersonConvinced = false;

    Vector3 playerLastPos;
    Vector3 personLastPos;

    public Vector3 playerConvincePos;
    public Vector3 personConvincePos;
    public Transform convinceAreaTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (convincing)
            {
                ThrowDice();
                Invoke("CalculateDieAmount", 1f);
                Invoke("CheckIfPersonConvinced", 2f);
                Invoke("ReturnToLevel", 3f);
                convincing = false;
            }
        }
    }

    public void StartConvinceMode(Person person)
    {
        currentPerson = person;
        newPersonConvinced = false;
        convincing = true;

        //PlayerStuff
        CharacterController player = GameManager.Instance.playerController;
        //set player position
        playerLastPos = player.transform.position;
        player.transform.position = playerConvincePos + convinceAreaTransform.position;

        //disable player movement
        player.enabled = false;

        //set followers positions
        GameManager.Instance.playerFollowers.EnableConvinceMode();

        //set person position
        personLastPos = currentPerson.transform.position;
        currentPerson.transform.position = personConvincePos + convinceAreaTransform.position;

        //set camera position
        Camera.main.transform.position = convinceAreaTransform.position + Camera.main.GetComponent<CameraFollow>().offset;
        Camera.main.GetComponent<CameraFollow>().enabled = false;
    }

    void ThrowDice()
    {
        GameManager.Instance.dieManager.ThrowDie(6);
        foreach (Follower followerDie in GameManager.Instance.playerFollowers.GetAllFollowers())
        {
            GameManager.Instance.dieManager.ThrowDie(followerDie.GetDieValue());
        }
    }

    void CalculateDieAmount()
    {
        dieAmount = 0;
        dieAmount += Random.Range(0, 6) + 1; //playerDie

        foreach (Follower followerDie in GameManager.Instance.playerFollowers.GetAllFollowers())
        {
            dieAmount += Random.Range(0, followerDie.GetDieValue()) + 1;
        }
    }

    void CheckIfPersonConvinced()
    {
        if (dieAmount >= currentPerson.convinceAmount)
        {
            newPersonConvinced = true;
        }
        else
        {
            currentPerson.interactable = false;
        }
    }

    void ReturnToLevel()
    {
        //set player position
        GameManager.Instance.playerController.transform.position = playerLastPos;

        //enable player
        GameManager.Instance.playerController.enabled = true;

        //set followers positions
        GameManager.Instance.playerFollowers.DisableConvinceMode();
        //set person position
        currentPerson.transform.position = personLastPos;

        //person joins party
        if (newPersonConvinced)
        {
            GameManager.Instance.playerFollowers.AddToFollowerList(currentPerson.dieValue);
            GameManager.Instance.personSpawner.DestroyPerson(currentPerson);
        }

        //set camera position
        Camera.main.transform.position = GameManager.Instance.playerController.transform.position;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
    }
}
