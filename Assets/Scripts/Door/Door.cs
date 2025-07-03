using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int levelNo;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private SceneLoader sceneLoader;
    private bool canOpen;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private DialogueTrigger lockedDialogue;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        lockedDialogue = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if(canOpen && Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale == 1)
        {
            if(levelNo == 1 || levelNo == 2 && playerRespawn.isLevel1Complete() || levelNo == 3 && playerRespawn.isLevel2Complete())
            {
                anim.SetTrigger("DoorOpen");
                SoundManager.instance.PlaySound(doorOpen);
            }
            else
            {
                lockedDialogue.CallManager();
            }
        }
    }

    public void Initialise(PlayerRespawn p, SceneLoader s)
    {
        playerRespawn = p;
        sceneLoader = s;
    }

    public void AnimEventEnumHandler()
    {
        StartCoroutine(LoadLevelOnOpen());
    }

    public IEnumerator LoadLevelOnOpen()
    {
        yield return new WaitForSeconds(0.1f);
        sceneLoader.SetSceneForLoad(levelNo + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            canOpen = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canOpen = false;
    }

}
