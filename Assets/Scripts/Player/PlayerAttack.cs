using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // the amount of time that needs to pass before another attack can be fired
    [SerializeField] private Transform firePoint; // the position from which the bullets are fired
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; // check if the attack cooldown is bigger than cooldown timer. Initialised to infinity to avoid starting the game with a cooldown period.
    private int damage = 1;
    private bool upgraded = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!upgraded)
        {
            if(Input.GetKeyDown(KeyCode.Z) && cooldownTimer > attackCooldown && Time.timeScale == 1)
            {
                Attack();
            }
        }
        else if(upgraded)
        {
            if(Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && Time.timeScale == 1)
            {
                Attack();
            }
        }
        cooldownTimer += Time.deltaTime; // increment the cooldown timer frame-perfectly
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0; // reset the cooldown timer when an attack is launched

        // Every time the player attacks, one of the array fireballs will be reset to the position of firePoint
        fireballs[FindFireball()].transform.position = firePoint.position;

        // Get projectile component from fireball, and shoot the fireball in the direction the player is facing 
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    // Check to see if the fireball at the specified array index is already in use, and if not,
    // return it, allowing the attack method to use it
    private int FindFireball()
    {
        for (int i=0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public int getDamage()
    {
        return damage;
    }

    // Call this method to make projectiles do more damage when player buys the corresponding upgrade
    public void setDamageOnUpgrade()
    {
        damage++;
    }

    public void setSecondUpgraded()
    {
        upgraded = true;
    }
}
