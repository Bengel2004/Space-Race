using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    // Plaats dit script op de AudioSlider
    Slider audioSlider;
    public TextMeshProUGUI value;

    void Start()
    {
        audioSlider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey("aVolume"))
            audioSlider.value = PlayerPrefs.GetFloat("aVolume");
        else
        {
            PlayerPrefs.SetFloat("aVolume", .5f);
            audioSlider.value = PlayerPrefs.GetFloat("aVolume");
        }
    }

    void Update()
    {
        value.text = Mathf.Round((audioSlider.value * 100)).ToString() + "%";
    }

    public void updateAudioVolumeSlider()
    {
        AudioManager.Instance.updateVolume(audioSlider.value);
    }

}
