using System.Collections;
using UnityEngine;
// Ivar & Hilmir

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _growAnimDeath;
    [SerializeField] public EnumPlayerAnimation EnumPlayerAnimation;

    private float AnimationDelay(EnumPlayerAnimation playerAnimation) {
        switch (playerAnimation)
        {
            case EnumPlayerAnimation.None:
                return 0f;
            case EnumPlayerAnimation.GrowDeath:
                return 1.5f; // Returns a value of 1 second.
            default:
                return 0f;
        }
    }

    public IEnumerator PlayAnimation(EnumPlayerAnimation playerAnimation, EnumPlayerTag tag) {

        switch (playerAnimation)
        {

            case EnumPlayerAnimation.None:
                break;
            case EnumPlayerAnimation.GrowDeath:
                PlayGrowDeath(tag);
                yield return new WaitForSeconds(AnimationDelay(playerAnimation)); // Plays for the returned value in AnimationDelay before breaking and respawning the player.
                break;
        }
    }

        private void PlayGrowDeath(EnumPlayerTag tag) {
        Transform transform = GameManager.Instance.GetPlayerReferences(tag).transform;
        GameObject growPowerUp = Instantiate(_growAnimDeath, transform.position, Quaternion.identity);
        growPowerUp.GetComponent<AnimationColorController>().ApplyColor(GameManager.Instance.GetPlayerReferences(tag).SkinController.GetMaterial());
        //play animation
    }
}
