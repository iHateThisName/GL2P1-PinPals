using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    private bool _masterMute = false, _musicMute = false, _sFXMute = false;

    public void SetMasterMute(bool value) => _masterMute = value;
    public void SetMusicMute(bool value) => _musicMute = value;
    public void SetSFXMute(bool value) => _sFXMute = value;
    public float MasterVolume {
        get {
            if (_masterMute) {
                return 0f;
            }
            Debug.Log("GET : " + PlayerSettings.RawMasterVolume);
            return PlayerSettings.RawMasterVolume;
        }
        set {
            Debug.Log("SET : " + value);
            PlayerSettings.RawMasterVolume = value;
        }
    }
    public float MusicVolume {
        get {
            if (_musicMute) {
                return 0f;
            }
            return ApplyMasterVolume(PlayerSettings.RawMusicVolume);
        }
        set => PlayerSettings.RawMusicVolume = Mathf.Clamp(value, 0f, 1f);
    }
    public float SFXVolume {
        get {
            if (_sFXMute) {
                return 0f;
            }
            return ApplyMasterVolume(PlayerSettings.RawSFXVolume);
        }
        set => PlayerSettings.RawSFXVolume = Mathf.Clamp(value, 0f, 1f);
    }

    private float ApplyMasterVolume(float rawVolume) {
        return MasterVolume * rawVolume;
    }
}


