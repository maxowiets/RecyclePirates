using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampQuoteSpeechBubble : MonoBehaviour
{
    public Animator anim;
    public List<string> quotes;
    public TextMeshPro text;

    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 1);
    }

    public void PopSpeechBubble()
    {
        CancelInvoke();
        anim.SetTrigger("Start");

        text.text = quotes[Random.Range(0, quotes.Count)];

        Invoke("StopSpeechBubble", 1.3f);
    }

    void StopSpeechBubble()
    {
        anim.SetTrigger("Stop");
    }
}
