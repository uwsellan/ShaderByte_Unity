using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UnityEngine.UI.Image HealthBarTotal;
    [SerializeField] private UnityEngine.UI.Image HealthBarCurrent;

    private void Start()
    {
        HealthBarTotal.fillAmount = playerHealth.currentHealth / 10; // health bar image has 10 hearts maximum so divide by 10 if a smaller total is to be used
    }

    private void Update()
    {
        HealthBarTotal.fillAmount = playerHealth.getStartingHealth() / 10;
        HealthBarCurrent.fillAmount = playerHealth.currentHealth / 10;
    }
}
