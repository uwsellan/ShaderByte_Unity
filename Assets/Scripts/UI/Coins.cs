using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    [SerializeField] private Money money;
    private Text textField;

    private void Awake()
    {
        textField = GetComponent<Text>();
    }

    private void Update()
    {
        textField.text = "x " + money.getCurrentMoney();
    }
}
