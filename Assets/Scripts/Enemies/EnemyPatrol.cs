using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Boundaries")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float enemySpeed;
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                moveInDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            {
                moveInDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    // Method to turn the enemy around when it reaches the boundary
    private void ChangeDirection()
    {
        anim.SetBool("Moving", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void moveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("Moving", true);

        // Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        // Move in the chosen direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed, enemy.position.y, enemy.position.z);
    }
}
