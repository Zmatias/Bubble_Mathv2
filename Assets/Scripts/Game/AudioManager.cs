using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource music;
    public AudioSource voiceover;

    public AudioClip[] voiceoverClips;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayVoiceoverClip(int clipIndex)
    {
        voiceover.PlayOneShot(voiceoverClips[clipIndex]);
    }
}
