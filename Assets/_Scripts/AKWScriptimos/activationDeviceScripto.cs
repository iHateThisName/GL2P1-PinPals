using UnityEngine;
//an AKW Script
public class activationDeviceScripto : MonoBehaviour
{
    public bool isActivated = false;

    private Vector3 _downPosition;
    private Vector3 _UpPosition;

    public float movmentDistance = 5f;
    public float movmentSpeed = 5f;

    public GameObject[] activationKeys;

    private activatorKeyCode[] keyScripts;

    private float activationTimer = 0f;
    public float activationTime = 5f; 

    void Start()
    {
        _UpPosition = transform.position;
        _downPosition = new Vector3(transform.position.x, transform.position.y - movmentDistance, transform.position.z);
        transform.position = _downPosition;

        keyScripts = new activatorKeyCode[activationKeys.Length];

        for (int i = 0; i < activationKeys.Length; i++)
        {
            keyScripts[i] = activationKeys[i].GetComponent<activatorKeyCode>();
        }
    }

    void Update()
    {
        isActivated = true;
        foreach (var key in keyScripts)
        {
            if (key.IsIdle)
            {
                isActivated = false;
                break; 
            }
        }

        if (isActivated)
        {
            activationTimer += Time.deltaTime;
        }

        if (activationTimer >= activationTime)
        {
            isActivated = false;
            activationTimer = 0f;

            foreach (var key in keyScripts)
            {
                key.IsIdle = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (transform.position != _downPosition || transform.position != _UpPosition)
        {
            if (isActivated)
            {
                transform.position = Vector3.MoveTowards(transform.position, _UpPosition, Time.fixedDeltaTime * movmentSpeed);
                Debug.Log("suprise!");
            }

            if (!isActivated)
            {
                transform.position = Vector3.MoveTowards(transform.position, _downPosition, Time.fixedDeltaTime * movmentSpeed);
                //Debug.Log("hiding");
            }
        }
    }
}

