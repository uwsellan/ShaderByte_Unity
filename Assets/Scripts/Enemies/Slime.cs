using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private AudioClip slimeSound;
    private Rigidbody2D enemyRB;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float visionRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private Transform player;
    private BoxCollider2D playerCollider;
    private Animator anim;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (PlayerInSight())
        {
            FacePlayer();
            anim.SetTrigger("Bounce");
        }

        anim.SetBool("Grounded", isGrounded());
        Physics2D.IgnoreCollision(boxCollider, playerCollider);
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
            transform.localScale = new Vector3(-2, 2, 2);  // Right
        else
            transform.localScale = new Vector3(2, 2, 2); // Left
    }

    private bool isGrounded()
    {
        // BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance [how far underneath the player the box will go], layer mask)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); 

        return raycastHit.collider != null; // return false if no collision with the ground
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * visionRange * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * visionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void Bounce()
    {
        if(isGrounded())
        {
            SoundManager.instance.PlaySound(slimeSound);

            if (player.position.x > transform.position.x)
            {
                enemyRB.linearVelocity = Vector2.zero;
                enemyRB.AddForce(new Vector2(2, 3.5f), ForceMode2D.Impulse);
            }
            else
            {
                enemyRB.linearVelocity = Vector2.zero;
                enemyRB.AddForce(new Vector2(-2, 3.5f), ForceMode2D.Impulse);
            }
        }
    }
}