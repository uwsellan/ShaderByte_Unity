using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Use a singleton pattern to restrict the UIManager class to one instance in memory
    public static UIManager instance { get; private set; }

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject persistent;
    [SerializeField] private Health stopiFrame;
    [SerializeField] private GameObject fade;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject coins;
    [SerializeField] private AudioClip gameOverSound;


    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Shop")]
    [SerializeField] private GameObject shop;

    private void Awake()
    {
        instance = this;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        // If escape key pressed, unpause if already paused, or pause if not
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1 && !mainMenu.activeInHierarchy && !gameOverScreen.activeInHierarchy && !shop.activeInHierarchy)
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over
    public void GameOver()
    {
        Time.timeScale = 0;
        healthBar.SetActive(false);
        lives.SetActive(false);
        coins.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);

        if(stopiFrame.GetCoroutine() != null)
            stopiFrame.stopIFramesOnRestart();

        Time.timeScale = 1;
        sceneLoader.Restart();
    }

    public void Quit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool _status)
    {
        pauseScreen.SetActive(_status);

        // Freeze the game when pause menu is displayed
        if(_status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion

    #region Shop

    public void StartShop()
    {
        Time.timeScale = 0;
        fade.SetActive(false);
        shop.SetActive(true);
    }

    #endregion
}
