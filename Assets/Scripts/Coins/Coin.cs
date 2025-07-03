using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float coinValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Money>().coinCollected(coinValue);
            gameObject.SetActive(false); // remove the collectible after it has been collected
        }

        // Player and enemy fireballs will shoot through the coins, and not collide with them
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(7, 9);
    }
}
