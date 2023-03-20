using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI diceAmountText;

    int convinced;

    private void Start()
    {
        diceAmountText.gameObject.SetActive(false);
        convinceUI.SetActive(false);
    }

    public void StartConvinceMode(Person person)
    {
        if (GameManager.Instance.convoMode == ConvoMode.POSTBATTLE || GameManager.Instance.energyManager.currentEnergy <= 0)
        {
            ReturnToLevel();
            return;
        }
        else if (GameManager.Instance.convoMode == ConvoMode.INBATTLE || GameManager.Instance.convoMode == ConvoMode.PREPREBATTLE)
        {
            Invoke("EnableUI", 0.1f);
            return;
        }
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

        //Preprebattle Text
        if (GameManager.Instance.convoMode == ConvoMode.PREBATTLE)
        {
            CanContinueConvinving();
            return;
        }

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
        Invoke("CanContinueConvinving", 6f);
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

        diceAmountText.gameObject.SetActive(true);
        diceAmountText.text = dieAmount.ToString(); 
    }

    void CheckIfPersonConvinced()
    {
        convinced = currentPerson.CheckIfConvinced(dieAmount);

        if (convinced == 3)
        {
            GameManager.Instance.energyManager.IncreaseEnergy();
        }
        else if (convinced == 2)
        {
            //
        }
        else if (convinced == 1)
        {
            GameManager.Instance.energyManager.DecreaseEnergy();
        }
    }

    void CanContinueConvinving()
    {
        diceAmountText.gameObject.SetActive(false);
        if (GameManager.Instance.convoMode == ConvoMode.PREBATTLE)
        {
            GameManager.Instance.convoMode = ConvoMode.PREPREBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.prePreBattleDialog[Random.Range(0, currentPerson.prePreBattleDialog.Count)];
            GameManager.Instance.ContinueConversation();
            return;
        }
        //if totally convinced
        else if (currentPerson.mentalState >= 3)
        {
            GameManager.Instance.convoMode = ConvoMode.POSTBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.postBattleGoodDialog[Random.Range(0, currentPerson.postBattleGoodDialog.Count)];
            //Increase Commander EE
        }

        //if totally depressed
        else if (currentPerson.mentalState <= 0)
        {
            GameManager.Instance.convoMode = ConvoMode.POSTBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.postBattleBadDialog[Random.Range(0, currentPerson.postBattleBadDialog.Count)];
            //Decrease Commander EE
        }

        //if convinced
        else if (convinced == 3)
        {
            GameManager.Instance.convoMode = ConvoMode.INBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.goodDialog[Random.Range(0, currentPerson.goodDialog.Count)];
        }

        //if nothing
        else if (convinced == 2)
        {
            GameManager.Instance.convoMode = ConvoMode.INBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.badDialog[Random.Range(0, currentPerson.badDialog.Count)];
        }

        //if not convinced
        else if (convinced == 1)
        {
            GameManager.Instance.convoMode = ConvoMode.INBATTLE;
            GameManager.Instance.talkWithPerson.currentDialog = currentPerson.badDialog[Random.Range(0, currentPerson.badDialog.Count)];
        }

        GameManager.Instance.ContinueConversation();
    }

    public void ReturnToLevel()
    {
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
