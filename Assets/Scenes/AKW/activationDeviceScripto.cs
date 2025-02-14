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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _UpPosition = transform.position;
        _downPosition = new Vector3(transform.position.x, transform.position.y - movmentDistance, transform.position.z);
        transform.position = _downPosition;
    }

    private void Update()
    {
        if (activationKey1.IsIdle = false)
        {
            isActivated = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != _downPosition || transform.position != _UpPosition)
        {
            if (isActivated == true)
            {
                //transform.position = _UpPosition;
                transform.position = Vector3.MoveTowards(transform.position, _UpPosition, Time.fixedDeltaTime * movmentSpeed);
                Debug.Log("suprise!");
            }

            if (isActivated == false)
            {
                //transform.position = _downPosition;
                transform.position = Vector3.MoveTowards(transform.position, _downPosition, Time.fixedDeltaTime * movmentSpeed);
                Debug.Log("hiding");
            }
        }
    }
}
