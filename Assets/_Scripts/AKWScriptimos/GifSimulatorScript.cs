using UnityEngine;
//an AKW Script
public class GifSimulatorScript : MonoBehaviour
{
    [Header("Animation Frames")]
    public Sprite[] frames;

    [Header("Settings")]
    public float framesPerSecond = 10f;
    public bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("GifSim, No Sprite");
            enabled = false;
        }
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f / framesPerSecond;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    enabled = false;
                    return;
                }
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
