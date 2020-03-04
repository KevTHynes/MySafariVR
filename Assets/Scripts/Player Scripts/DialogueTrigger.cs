using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager trigger;

    public string title;

    private void Awake()
    {
        trigger = GetComponentInParent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (title != null)
            {
                trigger.PlayDialogue(title);
            }
        }
        
    }
}
