using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    MAIN,
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType _soundType, float _volume)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)_soundType], _volume);
    }
}
