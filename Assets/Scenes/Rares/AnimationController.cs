using UnityEngine;

public class TAnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // Change the condition
        {
            animator.SetTrigger("PlayAnimationPlayer1");
        }

        if (Input.GetKeyDown(KeyCode.S)) // Change the condition
        {
            animator.SetTrigger("PlayAnimationPlayer2");
        }

        if (Input.GetKeyDown(KeyCode.D)) // Change the condition
        {
            animator.SetTrigger("PlayAnimationPlayer3");
        }

        if (Input.GetKeyDown(KeyCode.F)) // Change the condition
        {
            animator.SetTrigger("PlayAnimationPlayer4");
        }
    }
}
