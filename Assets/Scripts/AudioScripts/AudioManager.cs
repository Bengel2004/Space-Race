using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Plaats dit script op een empty gameObject. Hij gaat automatisch mee als je scene switcht.

    public List<AudioSource> allSources = new List<AudioSource>();
    public float audioVolume;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        // Je kan hier ook een scriptable object aanmaken en daarmee de bestaande waarde al invoeren die werkt evengoed als de playerprefs.
        // Maak dan bijv. een gameManager scriptable object.
        audioVolume = PlayerPrefs.GetFloat("aVolume"); 

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void updateVolume(float volume)
    {
        audioVolume = volume;
        PlayerPrefs.SetFloat("aVolume", volume);
        foreach (AudioSource source in allSources)
        {
            if (source != null)
                source.volume = volume;
            else
                allSources.Remove(source);
        }
    }
}
