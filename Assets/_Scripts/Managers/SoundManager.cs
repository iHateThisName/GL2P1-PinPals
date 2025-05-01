using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    public bool masterMute { get; private set; } = false;
    public bool musicMute { get; private set; } = false;
    public bool sfxMute { get; private set; } = false;
    public void SetMasterMute(bool value) => masterMute = value;
    public void SetMusicMute(bool value) => musicMute = value;
    public void SetSFXMute(bool value) => sfxMute = value;
    public float MasterVolume {
        get {
            if (masterMute) {
                return 0f;
            }
            return PlayerSettings.RawMasterVolume;
        }
        set {
            PlayerSettings.RawMasterVolume = Mathf.Clamp(value, 0f, 1f);
        }
    }
    public float MusicVolume {
        get {
            if (musicMute) {
                return 0f;
            }
            return PlayerSettings.RawMusicVolume;
        }
        set => PlayerSettings.RawMusicVolume = Mathf.Clamp(value, 0f, 1f);
    }

    public float SFXVolume {
        get {
            if (sfxMute) {
                return 0f;
            }
            return PlayerSettings.RawSFXVolume;
        }
        set => PlayerSettings.RawSFXVolume = Mathf.Clamp(value, 0f, 1f);
    }

    public float MusicVolumeWithMasterVolumeApplied() => ApplyMasterVolume(MusicVolume);
    public float SFXVolumeWithMasterVolumeApplied() => ApplyMasterVolume(SFXVolume);
    private float ApplyMasterVolume(float rawVolume) {
        return MasterVolume * rawVolume;
    }
}


