using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}
    public GameObject dialoguePanel;
    [SerializeField] public RectTransform dialogueRect;
    public Text dialogueText;

    public List<string> dialogueLines = new List<string>();
    private int currentLineIndex = 0;
    private bool dialogueActive = false;
    private Coroutine typingCoroutine;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.X))
        {
            if (typingCoroutine != null) // if coroutine is running, stop it and print remainder of line instantly
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                typingCoroutine = null;
            }
            else // all text from the previous line printed, either via coroutine or X input
            {
                NextLine();
            }
        }

        // Exit out of dialogue at any stage early without the rest being printed
        if (dialogueActive && Input.GetKeyDown(KeyCode.Z))
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            EndDialogue();
        }
    }

    public void StartDialogue(List<string> lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        dialogueActive = true;
        Time.timeScale = 0f;

        StartCoroutine(StretchPanelIn(0.3f));
    }

    // Method to activate the dialogue panel and gradually stretch it to full height from the midpoint
    IEnumerator StretchPanelIn(float duration)
    {
        dialoguePanel.SetActive(true);
        Vector3 startScale = new Vector3(1, 0, 1);
        Vector3 endScale = Vector3.one;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            dialogueRect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        ShowLine();
        dialogueRect.localScale = endScale;
    }


    void ShowLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    // Increment the index counter to be compared in the next ShowLine call
    void NextLine()
    {
        currentLineIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        StartCoroutine(CloseAfterDelay()); // Match animation length
    }

    // Reset the dialogue components to default states after the dialogue finishes, fading 
    IEnumerator CloseAfterDelay()
    {
        yield return StartCoroutine(StretchPanelOut(0.3f)); // this stops the player from jumping after clearing the last textbox
        dialogueText.text = "";
        dialogueActive = false;
        Time.timeScale = 1f;
    }

    // The opposite effect of StretchPanelIn, when the dialogue is finished
    IEnumerator StretchPanelOut(float duration)
    {
        dialogueText.text = null;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = new Vector3(1, 0, 1);
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            dialogueRect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        dialogueRect.localScale = endScale;
        dialoguePanel.SetActive(false);
    }

    // Iterate through the characters in the passed dialogue line to print it to the text field one character at a time
    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        typingCoroutine = null;
    }

    public bool isDialogueActive()
    {
        return dialogueActive;
    }
}
