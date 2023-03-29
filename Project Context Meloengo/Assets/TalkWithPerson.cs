using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkWithPerson : MonoBehaviour
{
    public List<DialogPreset> dialog;
    public DialogPreset currentDialog;
    public Animator anim;
    bool isTalking = false;
    int index;

    public GameObject speechBubble;
    public RawImage playerUI;
    public RawImage personUI;
    public TextMeshProUGUI speechText;

    private void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
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
        speechBubble.SetActive(false);
        GameManager.Instance.timeManager.SetRotationSpeed(0f);
        GameManager.Instance.playerMovement.enabled = false;
        GameManager.Instance.playerController.enabled = false;
        playerUI.material.color = new Color(0.3f, 0.3f, 0.3f);
        personUI.material.color = new Color(0.3f, 0.3f, 0.3f);
        personUI.texture = GameManager.Instance.currentPerson.characterImageUI;
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
        speechBubble.SetActive(true);
        speechText.text = currentDialog.dialogText[index];

        if (currentDialog.isPlayerTalking[index])
        {
            playerUI.material.color = Color.white;
            personUI.material.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            playerUI.material.color = new Color(0.3f, 0.3f, 0.3f);
            personUI.material.color = Color.white;
        }

        index++;
    }

    void StopConversation()
    {
        anim.SetTrigger("StopTalking");
        //reset variables
        isTalking = false;
        index = 0;
        speechBubble.SetActive(false);

        GameManager.Instance.Invoke("StartConvinceMode", 1f);
    }
}
