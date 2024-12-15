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
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType _soundType, float _volume)
    {
        if (instance.soundList == null || (int)_soundType >= instance.soundList.Length)
        {
            Debug.LogError($"SoundList is not properly configured or missing entry for {_soundType}");
            return;
        }

        instance.audioSource.PlayOneShot(instance.soundList[(int)_soundType], _volume);
    }

}
