using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float visionRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private bool isVendor;
    private Transform player;
    private Animator anim;
    private EnemyPatrol NPCPatrol;
    private DialogueTrigger NPCdialogue;

    private bool waitingToOpenShop = false;

    private bool playerTalk; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        NPCPatrol = GetComponentInParent<EnemyPatrol>();
        NPCdialogue = GetComponent<DialogueTrigger>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (PlayerInSight())
        {
            FacePlayer();
        }

        // Patrol only when the player is not close by
        if (NPCPatrol != null)
            NPCPatrol.enabled = !PlayerInSight();

        // If player is close by and down arrow is pressed, NPC will talk to the player via the dialogue system
        if (playerTalk && Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale == 1)
        {
            NPCdialogue.CallManager();

            // Buffer the shop system for vendor NPCs until the dialogue ends using waitingToOpenShop
            if (isVendor)
            {
                waitingToOpenShop = true;
            }
        } 

        // Open the shop after the vendor dialogue finishes, resetting the buffer variable
        if (waitingToOpenShop && !NPCdialogue.getDialogueActive())
        {
            waitingToOpenShop = false;
            UIManager.StartShop();
        }    
    }

    public void Initialise(UIManager ui)
    {
        UIManager = ui;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D seesPlayer = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * visionRange * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * visionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
        0, Vector2.left, 0, playerLayer);

        return seesPlayer.collider != null;
    }

    private void FacePlayer()
    {
        if(player == null) return;

        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1.5f, 1);  // Right
        else
            transform.localScale = new Vector3(-1, 1.5f, 1); // Left
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * visionRange * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * visionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerTalk = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerTalk = false;
    }
}
