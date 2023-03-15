using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkWithPerson : MonoBehaviour
{
    public List<DialogPreset> dialog;
    DialogPreset currentDialog;
    public Animator anim;
    bool isTalking = false;
    int index;

    public GameObject playerSpeechBubble;
    public GameObject personSpeechBubble;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI personText;

    private void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextDialog();
            }
        }
    }

    public void StartConversation()
    {
        anim.SetTrigger("StartTalking");
        //reset variables
        isTalking = false;
        index = 0;
        playerSpeechBubble.SetActive(false);
        personSpeechBubble.SetActive(false);
        currentDialog = dialog[Random.Range(0, dialog.Count)];
        GameManager.Instance.timeManager.SetRotationSpeed(0f);
        GameManager.Instance.playerMovement.enabled = false;
        GameManager.Instance.playerController.enabled = false;
        Invoke("NextDialog", 1f);
    }

    void NextDialog()
    {
        if (index >= currentDialog.isPlayerTalking.Count)
        {
            StopConversation();
            return;
        }
        isTalking = true;
        if (currentDialog.isPlayerTalking[index])
        {
            playerSpeechBubble.SetActive(true);
            personSpeechBubble.SetActive(false);
            playerText.text = currentDialog.dialogText[index];
        }
        else
        {
            playerSpeechBubble.SetActive(false);
            personSpeechBubble.SetActive(true);
            personText.text = currentDialog.dialogText[index];
        }

        index++;
    }

    void StopConversation()
    {
        anim.SetTrigger("StopTalking");
        //reset variables
        isTalking = false;
        index = 0;
        playerSpeechBubble.SetActive(false);
        personSpeechBubble.SetActive(false);

        GameManager.Instance.Invoke("StartConvinceMode", 1f);
    }
}
