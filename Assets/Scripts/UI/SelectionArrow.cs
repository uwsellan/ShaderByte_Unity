using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; // move cursor
    [SerializeField] private AudioClip interactSound; // when option selected
    private RectTransform rect;
    private int currentOption; // The position of the "cursor"

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Move cursor
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        // Interacting with options
        if(Input.GetKeyDown(KeyCode.X))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentOption += _change;

        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        // Only move the arrow if it's not at the top or bottom of the menu already
        if (currentOption < 0)
            currentOption = options.Length - 1;
        else if (currentOption > options.Length - 1)
            currentOption = 0;

        // Assign Y-position of current option to the cursor (move up/down)
        rect.position = new Vector3(rect.position.x, options[currentOption].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        // Access button component on a selected option to call its function
        options[currentOption].GetComponent<Button>().onClick.Invoke();
    }

}
