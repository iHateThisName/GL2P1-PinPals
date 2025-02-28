using System.Collections;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{

    [SerializeField] private AudioSource soundFXObject;

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawmTransform, float volume, float duration = 0.0f)
    {
        //spawn in GameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawmTransform.position, Quaternion.identity);
        //assign audioClip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after playing
        if (duration > 0.0f)
        {
            Destroy(audioSource.gameObject, duration);
        }
        else
        {
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
