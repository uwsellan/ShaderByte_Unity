using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private SceneLoader sceneLoader;

    private void Awake()
    {
        mainMenu.SetActive(true); // false for debug
        howToPlay.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(howToPlay.activeInHierarchy)
            {
                howToPlay.SetActive(false);
                mainMenu.SetActive(true);
            }
        }
    }

    public void Play() 
    {
        sceneLoader.Initiate();
    }

    public void HowToPlay() 
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void ChangeVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.25f);
    }

    public void ChangeSFXVol()
    {
        SoundManager.instance.ChangeSFXVolume(0.25f);
    }

    public void Quit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
