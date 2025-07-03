using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn Instance { get; private set; }

    [SerializeField] private GameObject resettable;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private AudioClip victory;
    [SerializeField] private SceneLoader sceneLoader;
    private Transform currentCheckpoint; // Most recently flagged checkpoint
    private Health playerHealth;
    private UIManager uiManager;
    [SerializeField] private int playerLives;
    [SerializeField] private Health[] enemies;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Animator playerAnim;
    private bool level1Complete = false;
    private bool level2Complete = false;

    private void Awake()
    {
        Instance = this;
        playerHealth = GetComponent<Health>();

        if(UIManager.instance != null)
            uiManager = UIManager.instance;
    }

    public void Respawn()
    {

        if(playerLives == 1)
        {
            uiManager.GameOver();
            player.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            if(currentCheckpoint != null)
            {
                transform.position = currentCheckpoint.position; // Move player to checkpoint position
            }
            playerLives--;
            playerHealth.Respawn(); // Restore player health and reset animation

            // Return the enemies to their original position in the level
            enemies = GameObject.FindObjectsByType<Health>(FindObjectsSortMode.None);

            foreach(Health enemy in enemies)
            {
                enemy.ResetPositionIfAlive();
            }

            // Reactivate all killed enemies and collected coins after the death (including in child components, where necessary)
            foreach(Transform obj in resettable.transform)
            {
                obj.gameObject.SetActive(true);

                var health = obj.GetComponent<Health>();

                    if(health != null)
                    {
                        health.Respawn();
                    }

                foreach(Transform child in obj.GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.SetActive(true);

                    var childHealth = child.GetComponent<Health>();

                    if(childHealth != null)
                    {
                        childHealth.Respawn();
                    }

                    // Handle the grandchildren objects on ranged enemies
                    if(child.gameObject.tag == "FirePoint")
                        child.gameObject.SetActive(true);
                    else if(child.gameObject.tag == "EnemyFireball")
                        child.gameObject.SetActive(false);
                }
            }
        }
    }

    // Activate checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "StartFlag")
        {
            currentCheckpoint = collision.transform; // Store checkpoint collided with as current checkpoint
            collision.GetComponent<Collider2D>().enabled = false; // Disable the checkpoint's boxcollider after first collision
        }

        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Store checkpoint collided with as current checkpoint
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Disable the checkpoint's boxcollider after first collision
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }

        if(collision.transform.tag == "Goal")
        {
            currentCheckpoint = null;
            SoundManager.instance.setMute();
            SoundManager.instance.PlaySound(victory);
            collision.GetComponent<Collider2D>().enabled = false; // Disable the checkpoint's boxcollider after first collision
            collision.GetComponent<Animator>().SetTrigger("Appear");

            playerHealth.RechargeHealth(playerHealth.getStartingHealth());
            
            // Set the level flag for the completed level, so the next level is unlocked and available to play
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                level1Complete = true;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                level2Complete = true;
            }

            StartCoroutine(levelComplete());
        }
    }

    public IEnumerator levelComplete()
    {
        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 1;
        sceneLoader.SetSceneForLoad(1);
    }

    public int getPlayerLives()
    {
        return playerLives;
    }

    public void setExtraLife()
    {
        playerLives++;
    }

    public bool isLevel1Complete()
    {
        return level1Complete;
    }
    public bool isLevel2Complete()
    {
        return level2Complete;
    }

    public void setResettable(GameObject _resettable)
    {
        resettable = _resettable;
    }

}