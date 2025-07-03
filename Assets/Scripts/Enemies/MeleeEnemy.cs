using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    [SerializeField] private AudioClip attackSound;

    [Header("Attacking Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int enemyDamage;
    [SerializeField] private float visionRange;

    [Header("Components")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("References")]
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();   
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && PlayerInSight())
        {
            cooldownTimer = 0;
            SoundManager.instance.PlaySound(attackSound);
            anim.SetTrigger("MeleeAttack");
        }

        // Enemy will patrol when player is not in sight, but stop to attack when player is in sight
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D seesPlayer = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * visionRange * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * visionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
        0, Vector2.left, 0, playerLayer);

        if(seesPlayer.collider != null)
            playerHealth = seesPlayer.transform.GetComponent<Health>();

        return seesPlayer.collider != null;
    }

    // Visualiser for enemy sight
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * visionRange * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * visionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight() && playerHealth.GetCoroutine() == null)
            playerHealth.TakeDamage(enemyDamage);
    }
}
