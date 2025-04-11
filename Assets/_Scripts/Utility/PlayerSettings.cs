public static class PlayerSettings {

    public static bool IsLandscape = true;
    public static string SelectedLevel = "Level 01, LasVegas";

    // Sounds
    public static float MasterVolume = 1f;
    private static float _worldVolume = 0.3f;
    private static float _sfxVolume = 0.5f;
    public static float WorldVolume {
        get => ApplyMasterVolume(_worldVolume);
        set => _worldVolume = value;
    }
    public static float SFXVolume {
        get => ApplyMasterVolume(_sfxVolume);
        set => _sfxVolume = value;
    }

    private static float ApplyMasterVolume(float rawVolume) {
        return MasterVolume * rawVolume;
    }


}
