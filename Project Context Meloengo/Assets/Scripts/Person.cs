using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int convinceAmount;
    public int dieValue;

    public TextMeshPro tmpValue;

    public bool interactable = true;

    public GameObject cloud;

    public int mentalState = 1;

    public GameObject bar;

    private void Start()
    {
        tmpValue.text = convinceAmount.ToString();
        tmpValue.gameObject.SetActive(false);

        mentalState = Random.Range(1, 3);
        SetBarSize();
        bar.SetActive(false);
    }

    public void ShowStats()
    {
        if (interactable) tmpValue.gameObject.SetActive(true);
    }

    public void HideStats()
    {
        tmpValue.gameObject.SetActive(false);
    }

    public int CheckIfConvinced(int thrownDiceAmount)
    {
        if (thrownDiceAmount >= convinceAmount)
        {
            IncreaseMentalState();
            return 3;
        }
        else if (thrownDiceAmount > (int)((float)convinceAmount * 0.7f))
        {
            return 2;
        }
        else
        {
            DecreaseMentalState();
            return 1;
        }
    }

    public void IncreaseMentalState()
    {
        mentalState++;
        SetBarSize();
    }

    public void DecreaseMentalState()
    {
        mentalState--;
        SetBarSize();
    }

    public void StartConvinceMode()
    {
        interactable = false;
        bar.SetActive(true);
    }

    public void ExitConvinceMode()
    {
        bar.SetActive(true);
        if (mentalState >= 3)
        {
            SucceedInteraction();
        }
        else if (mentalState >= 1)
        {
            NormalEndInteraction();
        }
        else if (mentalState <= 0)
        {
            FailInteraction();
        }
    }

    void SucceedInteraction()
    {
        GameManager.Instance.playerFollowers.AddToFollowerList(dieValue);
        GameManager.Instance.personSpawner.DestroyPerson(this);
    }

    void NormalEndInteraction()
    {
        HideStats();
    }

    void FailInteraction()
    {
        HideStats();
        cloud.SetActive(true);
    }

    void SetBarSize()
    {
        bar.transform.localScale = new Vector3(1, mentalState, 1);
    }

    private void OnDestroy()
    {
        HideStats();
    }
}
