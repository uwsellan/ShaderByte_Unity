using NUnit.Framework.Constraints;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private PlayerAttack playerAttack;
    private bool hit;
    private float direction;

    // Represents the time a projectile as been active, and deactivate it if it's been active for too long without a collision
    private float lifetime;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;

        if(lifetime > 0.4f)
            gameObject.SetActive(false);
    }

    // Check if fireball hit another object, and if so, update the appropriate variables and set explosion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if(collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(playerAttack.getDamage());
    }

    // Use the method every time a projectile is fired, to tell it whether to fire left or right.
    // Also used to reset the state of the fireball (hit false, boxcollider true)
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        // if fireball is NOT facing the same direction as the player, invert the fireball object 
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Method used to deactivate the fireball after the explosion animation has finished
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
