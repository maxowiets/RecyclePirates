using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatSpeechBubble : MonoBehaviour
{
    public Animator anim;
    public List<string> quotesHopeful;
    public List<string> quotesAnger;
    public List<string> quotesShame;
    public List<string> quotesImpotence;
    public TextMeshPro text;

    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 1);
    }

    public void PopSpeechBubble(int mentalStateValue)
    {
        CancelInvoke();
        anim.SetTrigger("Start");

        Debug.Log("POP BUBBLVE");
        switch (mentalStateValue)
        {
            case 0:
                text.text = quotesImpotence[Random.Range(0, quotesImpotence.Count)];
                break;
            case 1:
                text.text = quotesShame[Random.Range(0, quotesShame.Count)];
                break;
            case 2:
                text.text = quotesAnger[Random.Range(0, quotesAnger.Count)];
                break;
            case 3:
                text.text = quotesHopeful[Random.Range(0, quotesHopeful.Count)];
                break;
            default:
                break;
        }

        Invoke("StopSpeechBubble", 1.3f);
    }

    void StopSpeechBubble()
    {
        anim.SetTrigger("Stop");
    }
}
