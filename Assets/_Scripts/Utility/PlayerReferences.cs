using UnityEngine;

public class PlayerReferences : MonoBehaviour {
    public PlayerScoreTracker PlayerScoreTracker;
    public PlayerPowerController PlayerPowerController;
    public Camera PinballCamera;
    public Rigidbody rb;
    public SkinController SkinController;
    public PlayerFollowCanvasManager PlayerFollowCanvasManager;
    public PlayerStats PlayerStats;
    public PlayerTipUI playerTipUI;
    public PlayerAnimationController PlayerAnimationController;

    public EnumPlayerTag GetPlayerTag() {
        if (System.Enum.TryParse(this.gameObject.tag, out EnumPlayerTag parsedTag)) {
            return parsedTag;
        } else {
            Debug.LogError($"Invalid player tag: {this.gameObject.tag}");
            return EnumPlayerTag.None;
        }
    }

    public Collider GetPlayerCollider() {
        return this.gameObject.GetComponent<Collider>();
    }

    public MeshRenderer GetPlayerMeshRenderer() {
        return this.gameObject.GetComponent<MeshRenderer>();
    }
}
