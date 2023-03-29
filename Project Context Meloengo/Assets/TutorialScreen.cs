using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public bool interactable;
    int currentTutorialFrame;
    public List<GameObject> tutorialObjects;

    // Update is called once per frame
    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                NextFrame();
            }
        }
    }

    public void StartGame()
    {
        GameManager.Instance.timeManager.SetRotationSpeed(1);
        GameManager.Instance.playerController.enabled = true;
        GameManager.Instance.playerMovement.enabled = true;
        gameObject.SetActive(false);
    }

    void NextFrame()
    {
        tutorialObjects[currentTutorialFrame].SetActive(false);
        currentTutorialFrame++;
        if (currentTutorialFrame >= tutorialObjects.Count)
        {
            StartGame();
        }
        else
        {
            tutorialObjects[currentTutorialFrame].SetActive(true);
        }
    }

    void MakeInteractable()
    {
        interactable = true;
    }

    public void Initialize()
    {
        GameManager.Instance.timeManager.SetRotationSpeed(0);
        GameManager.Instance.playerController.enabled = false;
        GameManager.Instance.playerMovement.enabled = false;
        currentTutorialFrame = 0;
        foreach (GameObject tut in tutorialObjects)
        {
            tut.SetActive(false);
        }
        tutorialObjects[currentTutorialFrame].SetActive(true);
        Invoke("MakeInteractable", 1f);
    }
}
