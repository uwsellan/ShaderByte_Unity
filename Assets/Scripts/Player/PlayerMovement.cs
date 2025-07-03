using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private LayerMask groundLayer;

    // Object instance of the Player object's body stored in variable
    private Rigidbody2D body;

    // Creates an instance of the Player animations
    private Animator anim;

    private BoxCollider2D boxCollider;

    private float horizontalInput;
    private bool onDoor;

    // Serialising the field allows it to be edited directly in Unity instead of hard-coding it into the multiplier variable
    [SerializeField] private float speedMultiplier;

    [SerializeField] private float jumpPower;
    private bool doubleJumpUnlocked = false; //debug true
    private bool canDoubleJump = false;
    [SerializeField] private DialogueManager dialogueManager;

    // Awake called every time the script is loaded
    private void Awake()
    {
        // Grab references for rigidbody, animator and box collider from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale = Vector3.one;
    }

    // Update runs frame-perfectly, but not when you load the script. Used to check for left or right input
    private void Update()
    {
        // The GetAxis method moves +1 or -1 on each frame Left/A or Right/D is pressed. Variable to store this method dynamically.
        horizontalInput = Input.GetAxis("Horizontal");

        // If player moves right, flip player right. Else if player moves left, flip player left.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if(Input.GetKeyDown(KeyCode.X) && Time.timeScale == 1)
        {
            Jump();
        }

        // Adjustable jump height (you will start falling when jump key is released)

        if(Input.GetKeyUp(KeyCode.X) && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }
            body.linearVelocity = new Vector2(horizontalInput * speedMultiplier, body.linearVelocity.y);

        if(onDoor && Input.GetKeyDown(KeyCode.DownArrow))
            SceneManager.LoadScene(2);


        // Set animator parameters
        anim.SetBool("Moving", isMoving());
        anim.SetBool("Grounded", isGrounded());
    }

    private void Jump()
    {
        // double jump
        if (!isGrounded() && canDoubleJump)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);

            if (Time.timeScale == 1)
                SoundManager.instance.PlaySound(jumpSound);

            canDoubleJump = false;
        }

        // normal jump
        if (isGrounded())  
        { 
            if(doubleJumpUnlocked)
                canDoubleJump = true;

            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);

            if(Time.timeScale == 1)
                SoundManager.instance.PlaySound(jumpSound);  
        }

    }

    public bool isMoving()
    {
        if(horizontalInput != 0)
            return true;
        else
            return false;
    }

    // Uses ray-casting to fire a virtual laser in a certain direction. When this laser intersects the ground collider, you are grounded.
    // Box-casting works similarly, except using a box instead of a laser to check for collisions. A box the width of the player is used to detect if the player is standing
    // on a platform and have them fall through if they land in a pitfall.
    private bool isGrounded()
    {
        // BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance [how far underneath the player the box will go], layer mask)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); 

        return raycastHit.collider != null; // return false if no collision with the ground
    }

    public void unlockDoubleJump()
    {
        doubleJumpUnlocked = true;
    }

}
