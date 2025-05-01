using UnityEngine;

public static class PlayerSettings {

    public static bool IsLandscape = true;
    public static string SelectedLevel = "Level 01, LasVegas";

    #region Volume Settigns
    // PlayerPres Keys
    private const string MasterVolumeKey = "MasterVolume", MusicVolumeKey = "MusicVolume", SFXVolumeKey = "SFXVolume"; // PlayerPrefs Keys

    // Cached values (lazy-loaded)
    private static float? _rawMasterVolume = null;
    private static float? _rawMusicVolume = null;
    private static float? _rawSFXVolume = null;

    public static float RawMasterVolume {
        get {
            if (_rawMasterVolume == null) {
                _rawMasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            }
            return _rawMasterVolume.Value;
        }
        set {
            _rawMasterVolume = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat(MasterVolumeKey, _rawMasterVolume.Value);
            PlayerPrefs.Save();
        }
    }
    //public static float GetRawMasterVolume() {
    //    if (_rawMasterVolume == null) {
    //        _rawMasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
    //    }
    //    return _rawMasterVolume.Value;
    //}

    //public static void SetRawMasterVolume(float value) {
    //    _rawMasterVolume = Mathf.Clamp(value, 0f, 1f);
    //    PlayerPrefs.SetFloat(MasterVolumeKey, _rawMasterVolume.Value);
    //    PlayerPrefs.Save();
    //}

    public static float RawMusicVolume {
        get {
            if (_rawMusicVolume == null) {
                _rawMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            }
            return _rawMusicVolume.Value;
        }
        set {
            _rawMusicVolume = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat(MusicVolumeKey, _rawMusicVolume.Value);
        }
    }
    public static float RawSFXVolume {
        get {
            if (_rawSFXVolume == null) {
                _rawSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
            }
            return _rawSFXVolume.Value;
        }
        set {
            _rawSFXVolume = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat(SFXVolumeKey, _rawSFXVolume.Value);
        }
    }
    #endregion
}
