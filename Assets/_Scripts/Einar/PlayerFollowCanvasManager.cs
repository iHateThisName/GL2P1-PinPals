using TMPro;
using UnityEngine;
public class PlayerFollowCanvasManager : MonoBehaviour {
    [SerializeField] private ModelController _modelController;
    [SerializeField] TMP_Text playerText;
    [SerializeField] public GameObject powerUpDisplayer;
    [SerializeField] private Sprite _multi, _shrink, _grow, _freeze, _bomb, _mine, _glue;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private RandomMaterial randomMaterial;
    private void Start() {
        EnumPlayerTag tag = _modelController.GetPlayerTag();
        playerText.text = "P" + (int)tag;
        playerText.color = randomMaterial.AssignedMaterialColor;
    }

    public void UpdateTextColor() {
        EnumPlayerTag tag = _modelController.GetPlayerTag();
        playerText.text = "P" + (int)tag;
        playerText.color = randomMaterial.AssignedMaterialColor;
    }

    public void DisplayPower(EnumPowerUp powerUp) {
        switch (powerUp) {
            case EnumPowerUp.None:
                DisableSprite();
                break;

            case EnumPowerUp.Shrink:
                spriteRenderer.sprite = _shrink;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.Grow:
                spriteRenderer.sprite = _grow;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.MultiBall:
                spriteRenderer.sprite = _multi;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.Freeze:
                spriteRenderer.sprite = _freeze;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.Bomb:
                spriteRenderer.sprite = _bomb;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.Mine:
                spriteRenderer.sprite = _mine;
                powerUpDisplayer.SetActive(true);
                break;

            case EnumPowerUp.Honey:
                spriteRenderer.sprite = _glue;
                powerUpDisplayer.SetActive(true);
                break;

            default:
                DisableSprite();
                break;
        }
    }

    public void MultiBallSprite() {
        spriteRenderer.sprite = _multi;
        powerUpDisplayer.SetActive(true);
    }

    public void DisableSprite() {
        spriteRenderer.sprite = null;
        powerUpDisplayer.SetActive(false);
    }
}
