using UnityEngine;

public class TAnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // When space is pressed
        {
            animator.SetTrigger("PlayAnimation"); // Set the trigger
        }
    }
}
