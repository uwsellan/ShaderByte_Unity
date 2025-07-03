using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{

    [SerializeField] private Money playerMoney;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float loot;
    public float currentHealth { get; private set; } // Can make getting public but setting private only in C#
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float extraInvincibilityTime; // the extra time after the flashes you are invincible for - if 0, take damage again as soon as flashes end
    [SerializeField] private int playerFlashCount;
    private SpriteRenderer spriteRend;
    private Coroutine iFrameCoroutine;

    [Header("Components")]
    [SerializeField] private Component[] components;
    private Rigidbody2D body;

    [Header("Transform")]
    private Vector3 startingPosition;
    private Vector3 startingScale;

    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

        startingPosition = transform.position;
        startingScale = transform.localScale;
    }

    public void Initialise(Money m)
    {
        playerMoney = m;
    }

    // Method to remove health from the player if they take damage
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // ensures health does not go below 0 or above maximum possible health value

        // Check if player dies after the damage
        if (currentHealth > 0)
        {
            // if take damage but still alive
            anim.SetTrigger("Hurt");
            SoundManager.instance.PlaySound(hurtSound);
            
            if(iFrameCoroutine != null)
                StopCoroutine(iFrameCoroutine);
        
            iFrameCoroutine = StartCoroutine(iFrame());
        }
        else
        {
            // die
            if(!dead)
            {
                anim.ResetTrigger("Hurt");
                anim.SetTrigger("Die");

                // Give the player coins according to the loot if an enemy dies
                if(CompareTag("Enemy"))
                    playerMoney.setLoot(loot);
                
                // Deactivate attached scripts and boxcolliders from dead objects
                foreach (Component component in components)
                {
                    if(component is Behaviour behaviour)
                        behaviour.enabled = false;
                    else if(component is Collider2D collider2D)
                        collider2D.enabled = false;
                }

                // Handle rigidbodies of player and slime upon their deaths
                if (body != null)
                {
                    body.linearVelocity = Vector2.zero;
                    body.bodyType = RigidbodyType2D.Static;

                    anim.SetBool("Grounded", true);
                }

                // Deactivate hitbox from slime (child object)
                Transform child = transform.Find("Hitbox");
                if(child != null)
                    child.gameObject.SetActive(false);

                SoundManager.instance.PlaySound(deathSound);
                dead = true;
            }
        }
    }

    public void RechargeHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        RechargeHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");

        if(iFrameCoroutine != null)
            StopCoroutine(iFrameCoroutine);
        
        iFrameCoroutine = StartCoroutine(iFrame());

        // Reactivate all attached component classes after respawn
        foreach (Component component in components)
        {
            if(component is Behaviour behaviour)
                behaviour.enabled = true;
            else if(component is Collider2D collider2D)
                collider2D.enabled = true;
        }

        if(body != null)
        {
            body.bodyType = RigidbodyType2D.Dynamic;
        }

        // Reactivate hitbox from slime (child object)
        Transform child = transform.Find("Hitbox");
        if(child != null)
            child.gameObject.SetActive(true);

        // Return the enemy to its original position
        if(CompareTag("Enemy"))
        {
            transform.position = startingPosition;
            transform.localScale = startingScale;

            if(body != null)
                body.linearVelocity = Vector2.zero;
        }
    }

    // Method to reposition the enemy upon player death, if it moved in the scene but did not die
    public void ResetPositionIfAlive()
    {
        if(CompareTag("Enemy"))
        {
            transform.position = startingPosition;
            transform.localScale = startingScale;

            if(body != null)
                body.linearVelocity = Vector2.zero;
        }
    }

    // Coroutine to handle invincibility frames after the player takes damage
    private IEnumerator iFrame()
    {   
        // Ignores the collision from player (layer 6) on enemy (layer 7) during the invincibility frame period 
        Physics2D.IgnoreLayerCollision(6, 7, true);   

        // loop to flash player transparent red and white
        for(int i=0; i < playerFlashCount; i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f); // change colour to transparent red

            // change colour back to white based on the formula shown in argument
            yield return new WaitForSeconds(0.2f);
            spriteRend.color = Color.white;

            // wait the same period before looping again
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(extraInvincibilityTime);

        Physics2D.IgnoreLayerCollision(6, 7, false); // reinstate collision detection as normal at end of coroutine
        iFrameCoroutine = null;
    }

    public void stopIFramesOnRestart()
    {
        if(iFrameCoroutine != null)
        {
            StopCoroutine(iFrameCoroutine);
            iFrameCoroutine = null;
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public Coroutine GetCoroutine()
    {
        return iFrameCoroutine;
    }

    public void setCoroutine()
    {
        iFrameCoroutine = null;
    }

    public float getStartingHealth()
    {
        return startingHealth;
    }

    public void setStartingHealth(float _health)
    {
        startingHealth = _health;
        currentHealth = startingHealth;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}