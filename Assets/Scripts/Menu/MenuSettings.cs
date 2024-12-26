using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private float saveVolume = 0f;

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        DataPersistenceManager.instance.SaveGame();
    }


    void Start()
    {
        saveVolume = PlayerPrefs.GetFloat("volume", -80f);
        volumeSlider.value = saveVolume;
        SetVolume(saveVolume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("volume", volume); // Настройка громкости через AudioMixer
    }
}
