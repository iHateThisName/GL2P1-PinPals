using UnityEngine;

public class ModelController : MonoBehaviour {
    public PlayerScoreTracker PlayerScoreTracker;
    public PlayerPowerController PlayerPowerController;
    public Camera PinballCamera;
    public Rigidbody rb;
    public SkinController SkinManager;
    public PlayerFollowCanvasManager PlayerFollowCanvasManager;

    public EnumPlayerTag GetPlayerTag() {
        if (System.Enum.TryParse(this.gameObject.tag, out EnumPlayerTag parsedTag)) {
            return parsedTag;
        } else {
            Debug.LogError($"Invalid player tag: {this.gameObject.tag}");
            return EnumPlayerTag.None;
        }
    }
}
