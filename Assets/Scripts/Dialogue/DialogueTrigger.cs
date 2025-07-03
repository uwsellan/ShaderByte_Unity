using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public DialogueManager dialogueManager;
    [SerializeField] public List<string> lines = new List<string>();

    public void Initialise(DialogueManager dm)
    {
        dialogueManager = dm;
    }

    public void CallManager()
    {
        dialogueManager.StartDialogue(lines);   
    }

    public bool getDialogueActive()
    {
        return dialogueManager.isDialogueActive();
    }
}

