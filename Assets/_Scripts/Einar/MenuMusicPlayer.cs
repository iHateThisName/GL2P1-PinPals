using UnityEngine;
using UnityEngine.SceneManagement;
//Einar
public class MenuMusicPlayer : MonoBehaviour
{
    public static MenuMusicPlayer Instance;

    public AudioSource audioSource;
    public AudioClip menuMusic;

    private bool isPlaying = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Play menu music if not already playing
        if (!isPlaying)
        {
            PlayMusic(menuMusic);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneName.StartsWith("Level 0"))
        {
            Destroy(gameObject);
        }
        // Optionally, you can check scene name or add logic
        // For menu scenes, keep the music playing
        // For other scenes, you might switch tracks or fade out
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            StartCoroutine(FadeOutIn(clip));
        }
    }

    private System.Collections.IEnumerator FadeOutIn(AudioClip newClip)
    {
        float duration = 1f; // seconds
        float currentTime = 0f;

        // Fade out
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1f, 0f, currentTime / duration);
            yield return null;
        }

        // Change clip
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
            yield return null;
        }
    }
}
