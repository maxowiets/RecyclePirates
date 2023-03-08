using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvinceMode : MonoBehaviour
{
    int dieAmount = 0;

    Person currentPerson;

    Vector3 playerLastPos;
    Vector3 personLastPos;

    public Vector3 playerConvincePos;
    public Vector3 personConvincePos;
    public Transform convinceAreaTransform;

    public GameObject convinceUI;

    private void Start()
    {
        convinceUI.SetActive(false);
    }

    public void StartConvinceMode(Person person)
    {
        currentPerson = person;

        //PlayerStuff
        PlayerMovement player = GameManager.Instance.playerMovement;
        //set player position
        playerLastPos = player.transform.position;
        player.transform.position = playerConvincePos + convinceAreaTransform.position;

        //disable player movement
        player.enabled = false;
        GameManager.Instance.playerController.enabled = false;

        //set followers positions
        GameManager.Instance.playerFollowers.EnableConvinceMode();

        //set person position
        personLastPos = currentPerson.transform.position;
        currentPerson.transform.position = personConvincePos + convinceAreaTransform.position;
        currentPerson.StartConvinceMode();

        //set camera position
        Camera.main.transform.position = convinceAreaTransform.position + Camera.main.GetComponent<CameraFollow>().offset;
        Camera.main.GetComponent<CameraFollow>().enabled = false;

        //set time multiplier to 0
        GameManager.Instance.timeManager.SetRotationSpeed(0);

        //enable ui
        Invoke("EnableUI", 0.1f);
    }

    void EnableUI()
    {
        convinceUI.SetActive(true);
    }

    public void Convince()
    {
        convinceUI.SetActive(false);
        Invoke("ThrowDice", 1f);
        Invoke("CalculateDieAmount", 2f);
        Invoke("CheckIfPersonConvinced", 3f);
        Invoke("CanContinueConvinving", 4f);
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
        int conv = currentPerson.CheckIfConvinced(dieAmount);

        if (conv == 3)
        {
            GameManager.Instance.energyManager.IncreaseEnergy();
        }
        else if (conv == 2)
        {
            //
        }
        else if (conv == 1)
        {
            GameManager.Instance.energyManager.UseEnergy();
        }
    }

    void CanContinueConvinving()
    {
        //Exit Convince Mode if...
        if (GameManager.Instance.energyManager.currentEnergy == 0 || currentPerson.mentalState >= 3 || currentPerson.mentalState <= 0)
        {
            if (currentPerson.mentalState >= 3)
            {
                //Increase commanders EE
            }
            else if (currentPerson.mentalState <= 0)
            {
                //Decrease commanders EE
            }
            ReturnToLevel();
        }

        //Continue Convince Mode if...
        else if(GameManager.Instance.energyManager.currentEnergy > 0)
        {
            convinceUI.SetActive(true);
        }
    }

    public void ReturnToLevel()
    {
        Debug.Log("test");
        //set player position
        GameManager.Instance.playerMovement.transform.position = playerLastPos;

        //enable player
        GameManager.Instance.playerMovement.enabled = true;
        GameManager.Instance.playerController.enabled = true;

        //set followers positions
        GameManager.Instance.playerFollowers.DisableConvinceMode();
        //set person position
        currentPerson.transform.position = personLastPos;

        //person joins party
        currentPerson.ExitConvinceMode();

        //set camera position
        Camera.main.transform.position = GameManager.Instance.playerMovement.transform.position;
        Camera.main.GetComponent<CameraFollow>().enabled = true;

        //set time multiplier back to 1
        GameManager.Instance.timeManager.SetRotationSpeed(1);

        //disable ui
        convinceUI.SetActive(false);
    }
}
