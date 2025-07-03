using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] private PlayerRespawn playerRespawn;
    private Text textField;

    private void Awake()
    {
        textField = GetComponent<Text>();
    }

    private void Update()
    {
        textField.text = "x " + playerRespawn.getPlayerLives();
    }
}
