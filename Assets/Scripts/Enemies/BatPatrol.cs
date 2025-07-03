using UnityEngine;

public class BatPatrol : MonoBehaviour
{
    [Header("Patrol Boundaries")]
    [SerializeField] private Transform upperEdge;
    [SerializeField] private Transform lowerEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float enemySpeed;
    private Vector3 initialScale;
    private bool movingDown;

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
        if(movingDown)
        {
            if(enemy.position.y >= lowerEdge.position.y)
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
            if(enemy.position.y <= upperEdge.position.y)
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

    }

    // Method to turn the enemy around when it reaches the boundary
    private void ChangeDirection()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingDown = !movingDown;
    }

    private void moveInDirection(int _direction)
    {
        idleTimer = 0;

        // Move in the chosen direction
        enemy.position = new Vector3(enemy.position.x, enemy.position.y + Time.deltaTime * _direction * enemySpeed, enemy.position.z);
    }
}

