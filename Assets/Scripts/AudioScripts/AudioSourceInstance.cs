using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceInstance : MonoBehaviour
{
    /**
    Plaats dit script op elke gameObject met een audioSource;
    Hij regel alles automatisch.
    Bij meerdere audiosources assign elke source individueel via de inspector.

    **/
    public AudioSource thisAudio;

    void Start()
    {
        if(thisAudio == null)
            thisAudio = GetComponent<AudioSource>();

        AudioManager.Instance.allSources.Add(thisAudio);
        thisAudio.volume = AudioManager.Instance.audioVolume;
    }

    private void Update()
    {
        thisAudio.volume = AudioManager.Instance.audioVolume;
    }
}
