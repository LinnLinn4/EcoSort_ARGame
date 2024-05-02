using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance { get; private set; }
    AudioSource src;
    public AudioClip sfx1, sfx2, sfx3;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DropSfx()
    {
        src.clip = sfx1;
        src.Play();
    }

    public void WinSfx()
    {
        src.clip = sfx2;
        src.Play();
    }

    public void LoseSfx()
    {
        src.clip = sfx3;
        src.Play();
    }
}
