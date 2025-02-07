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
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("PlayAnimationPlayer1");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("PlayAnimationPlayer2");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("PlayAnimationPlayer3");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("PlayAnimationPlayer4");
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetFloat("YPosition", transform.position.y);
            animator.SetTrigger("ResetToZeroTrigger");
        }
    }
}
