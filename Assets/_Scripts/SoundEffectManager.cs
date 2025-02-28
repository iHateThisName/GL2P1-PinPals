using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
   public static SoundEffectManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawmTransform, float volume)
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
        Destroy(audioSource.gameObject, clipLength);
    }
}
