using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float timer = 1f;
    private int state = 1;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            if (state == 1)
            {
                state = 2;
                meshRenderer.enabled = true;
                //Debug.Log("State = 2 ENABLED");
            }
            else
            {
                state = 1;
                meshRenderer.enabled = false;
                //Debug.Log("State = 1 DISABLED");
            }

            timer = 1f;
        }
    }
}