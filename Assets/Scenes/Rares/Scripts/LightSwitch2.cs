using UnityEngine;

public class LightSwitch2 : MonoBehaviour
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
            if (state == 2)
            {
                state = 1;
                meshRenderer.enabled = true;
                //Debug.Log("State = 2 ENABLED");
            }
            else
            {
                state = 2;
                meshRenderer.enabled = false;
                //Debug.Log("State = 1 DISABLED");
            }

            timer = 1f;
        }
    }
}