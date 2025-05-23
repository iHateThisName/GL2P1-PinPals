using System.Collections;
using UnityEngine;
// Ivar & Hilmir

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _growAnimDeath;
    [SerializeField] private GameObject _freezeEffect;
    [SerializeField] public EnumPlayerAnimation EnumPlayerAnimation;

    private float AnimationDelay(EnumPlayerAnimation playerAnimation)
    {
        switch (playerAnimation)
        {
            case EnumPlayerAnimation.None:
                return 0f;
            case EnumPlayerAnimation.GrowDeath:
                return 1.5f;
            case EnumPlayerAnimation.FreezeEffect:
                return 3f;
            default:
                return 0f;
        }
    }

    public IEnumerator PlayAnimation(EnumPlayerAnimation playerAnimation, EnumPlayerTag tag)
    {

        switch (playerAnimation)
        {

            case EnumPlayerAnimation.None:
                break;
            case EnumPlayerAnimation.GrowDeath:
                PlayGrowDeath(tag);
                yield return new WaitForSeconds(AnimationDelay(playerAnimation)); // Plays for the returned value in AnimationDelay before breaking and respawning the player.
                break;
            case EnumPlayerAnimation.FreezeEffect:
                GameObject freeze = PlayFreezeEffect(tag);
                yield return new WaitForSeconds(AnimationDelay(playerAnimation));
                Destroy(freeze);
                GameManager.Instance.ShowPlayer(tag, enableCamera: true, unfreezePlayer: true);
                break;
        }
    }

    private void PlayGrowDeath(EnumPlayerTag tag)
    {
        Transform transform = GameManager.Instance.GetPlayerReferences(tag).transform;
        GameObject growPowerUp = Instantiate(_growAnimDeath, transform.position, Quaternion.identity);
        growPowerUp.GetComponent<AnimationColorController>().ApplyColor(GameManager.Instance.GetPlayerReferences(tag).SkinController.GetMaterial());
    }
    //play animation

    public GameObject PlayFreezeEffect(EnumPlayerTag tag)
    {
        Transform transform = GameManager.Instance.GetPlayerReferences(tag).transform;
        GameManager.Instance.HidePlayer(tag, false);
        GameObject freezeEffect = Instantiate(_freezeEffect, transform.position, Quaternion.identity);
        return freezeEffect;
    }
}
