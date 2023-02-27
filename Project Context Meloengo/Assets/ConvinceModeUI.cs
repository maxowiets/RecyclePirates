using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvinceModeUI : MonoBehaviour
{
    public GameObject mainOptionsUI;
    public GameObject attackOptionsUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        mainOptionsUI.SetActive(true);
        attackOptionsUI.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log("test");
        mainOptionsUI.SetActive(false);
        attackOptionsUI.SetActive(false);
    }

    public void AttackOptions()
    {
        mainOptionsUI.SetActive(false);
        attackOptionsUI.SetActive(true);
    }

    public void RunAway()
    {
        Debug.Log("test1");
        this.enabled = false;
    }

    public void ReturnToOptions()
    {
        mainOptionsUI.SetActive(true);
        attackOptionsUI.SetActive(false);
    }

    public void Attack1()
    {
        Debug.Log("Attack1");
    }

    public void Attack2()
    {
        Debug.Log("Attack2");
    }

    public void Attack3()
    {
        Debug.Log("Attack3");
    }

    public void Attack4()
    {
        Debug.Log("Attack4");
    }
}
