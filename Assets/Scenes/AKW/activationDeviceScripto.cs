using UnityEngine;

public class activationDeviceScripto : MonoBehaviour
{
    public bool isActivated = false;

    private Vector3 _downPosition;
    private Vector3 _UpPosition;

    public float movmentDistance = 5f;
    public float movmentSpeed = 5f;

    public GameObject activationKey1;
    public GameObject activationKey2;
    public GameObject activationKey3;

    private activatorKeyCode key1;
    private activatorKeyCode key2;
    private activatorKeyCode key3;

    void Start()
    {
        _UpPosition = transform.position;
        _downPosition = new Vector3(transform.position.x, transform.position.y - movmentDistance, transform.position.z);
        transform.position = _downPosition;

        //references to key objects
        key1 = activationKey1.GetComponent<activatorKeyCode>();
        key2 = activationKey2.GetComponent<activatorKeyCode>();
        key3 = activationKey3.GetComponent<activatorKeyCode>();
    }

    void Update()
    {
        // Check if keys are not idle
        if (!key1.IsIdle && !key2.IsIdle && !key3.IsIdle)
        {
            isActivated = true;
        }
        else
        {
            isActivated = false;
        }
    }

    void FixedUpdate()
    {
        if (transform.position != _downPosition || transform.position != _UpPosition)
        {
            if (isActivated == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _UpPosition, Time.fixedDeltaTime * movmentSpeed);
                Debug.Log("suprise!");
            }

            if (isActivated == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _downPosition, Time.fixedDeltaTime * movmentSpeed);
                Debug.Log("hiding");
            }
        }
    }
}
