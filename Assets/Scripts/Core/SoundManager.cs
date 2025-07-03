using System.Data.Common;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   // Use a singleton pattern to restrict the SoundManager class to one instance in memory
   public static SoundManager instance { get; private set; }
   private AudioSource soundSource;
   private AudioSource musicSource;
   private float currentVolume;
   private float finalVolume;

    private void Awake()
    {
        instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Initialise volumes
        ChangeMusicVolume(0);
        ChangeSFXVolume(0);
    }

    // Play an audio clip only once when called (sound effects)
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void setMute()
    {
        musicSource.mute = true;
    }

    public void ChangeSFXVolume(float _change)
    {
        // Get base volume 
        float baseVolume = 1;

        // Get initial volume from PlayerPrefs and update it
        currentVolume = PlayerPrefs.GetFloat("soundVolume", 1);
        currentVolume += _change;

        // validate volume as being between 0 (0%) and 1 (100%)
        if(currentVolume > 1)
            currentVolume = 0;
        else if(currentVolume < 0)
            currentVolume = 1;

        // Assign final value and save to PlayerPrefs
        finalVolume = currentVolume * baseVolume;
        soundSource.volume = finalVolume;
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    public void ChangeMusicVolume(float _change)
    {
        // Get base volume 
        float baseVolume = 0.3f;

        // Get initial volume from PlayerPrefs and update it
        currentVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        currentVolume += _change;

        // validate volume as being between 0 (0%) and 1 (100%)
        if(currentVolume > 1)
            currentVolume = 0;
        else if(currentVolume < 0)
            currentVolume = 1;

        // Assign final value and save to PlayerPrefs
        finalVolume = currentVolume * baseVolume;
        musicSource.volume = finalVolume;
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}
