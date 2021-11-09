using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject SpeechBubble;
    public bool inRange = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange == true)
        {
            UI.SetActive(true);
        }
        if (inRange == false)
        {
            UI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpeechBubble.SetActive(true);
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpeechBubble.SetActive(false);
        inRange = false;
    }
}
