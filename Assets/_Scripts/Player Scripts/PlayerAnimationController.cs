using System.Collections;
using UnityEngine;
// Ivar & Hilmir

public class PlayerAnimationController : MonoBehaviour
{

    public enum EnumPlayerAnimation {
        None = 0,
        GrowDeath = 1,
    }

    private float AnimationDelay(EnumPlayerAnimation playerAnimation) {
        switch (playerAnimation)
        {
            case EnumPlayerAnimation.None:
                return 0f;
            case EnumPlayerAnimation.GrowDeath:
                return 1f;
            default:
                return 0f;
        }
    }

    public IEnumerator PlayAnimation(EnumPlayerAnimation playerAnimation) {

        switch (playerAnimation)
        {

            case EnumPlayerAnimation.None:
                break;
            case EnumPlayerAnimation.GrowDeath:
                PlayGrowDeath();
                yield return new WaitForSeconds(AnimationDelay(playerAnimation));
                break;
        }
    }

        private void PlayGrowDeath() {

        //play animation
    }
}
