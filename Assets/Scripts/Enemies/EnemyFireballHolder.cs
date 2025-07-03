using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{

    [SerializeField] private Transform enemy;

    private void Update()
    {
        // make fireball holder face the same direction as enemy at all times
        transform.localScale = enemy.localScale;
    }
}
