using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private GameObject persistent;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private Animator anim;
    private Vector3 playerSpawnPosition;

    [Header("UI Canvas Children")]

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mainMenuArrow;
    [SerializeField] private GameObject backdrop;

    void Awake()
    {
        Instance = this;
    }

    public void Initiate()
    {
        StartCoroutine(StartGame());   
    }

    // Set the game up (called from the button method above when a new game is started from the main menu)
    public IEnumerator StartGame()
    {
        mainMenuArrow.SetActive(false);
        DontDestroyOnLoad(persistent);
        yield return new WaitForSeconds(0.1f);

        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(false);
        backdrop.SetActive(false);

        SceneManager.LoadScene(1);
        anim.SetTrigger("Start");

        player.SetActive(true);
        money.SetActive(true);
        lives.SetActive(true);
        healthBar.SetActive(true);

        playerSpawnPosition = new Vector3(-19.46f, 7.25f, 0);
        player.transform.position = playerSpawnPosition;
    }

    public void Restart()
    {
        StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        Destroy(persistent);
        SceneManager.LoadScene(0);
    }

    public void SetSceneForLoad(int _sceneNo)
    {
        StartCoroutine(LoadScene(_sceneNo));
    }

    public IEnumerator LoadScene(int _sceneNo)
    {
        DontDestroyOnLoad(persistent);

        switch(_sceneNo)
        {
            case 1: // hub
                anim.SetTrigger("End");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(1);
                anim.SetTrigger("Start");

                playerSpawnPosition = new Vector3(-19.46f, 7.25f, 0);
                player.transform.position = playerSpawnPosition;
                break;

            case 2: // level 1
                anim.SetTrigger("End");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(2);
                anim.SetTrigger("Start");

                playerSpawnPosition = new Vector3(-21.868f, 3.295f, 0);
                player.transform.position = playerSpawnPosition;
                break;

            case 3: // level 2
                anim.SetTrigger("End");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(3);
                anim.SetTrigger("Start");

                playerSpawnPosition = new Vector3(-8.7f, -2f, 0);
                player.transform.position = playerSpawnPosition;
                break;

            case 4: // level 3
                anim.SetTrigger("End");
                yield return new WaitForSecondsRealtime(1);
                SceneManager.LoadScene(4);
                anim.SetTrigger("Start");

                playerSpawnPosition = new Vector3(51.02f, 14.68f, 0);
                player.transform.position = playerSpawnPosition;
                break;
        }
    }
}
