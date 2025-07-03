using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeType;
    [SerializeField] private string textIntro;
    private Text volumeText;

    private void Awake()
    {
        volumeText = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumePercentage = PlayerPrefs.GetFloat(volumeType) * 100;
        volumeText.text = textIntro + volumePercentage.ToString() + "%";
    }
}
