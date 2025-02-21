using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class PlayerFollowCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text playerText;
    //[SerializeField] Vector3 textOffset = new Vector3 (0, 0, 3);
    [SerializeField] public GameObject powerUpDisplayer;
    //[SerializeField] private EnumPowerUp powerUps;
    [SerializeField] private Sprite _multi, _shrink, _grow, _freeze;
    //public EnumPlayerTag playerTag { get; private set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private RandomMaterial randomMaterial;
    private void Start()
    {
        playerText.color = randomMaterial.AssignedMaterialColor;
        //playerText.text = $"{player.tag}";
        string firstChar = player.tag.Substring(0, 1);
        string lastChar = player.tag.Substring(player.tag.Length - 1);
        //int lastInt = int.Parse(lastChar);

        //playerText.text = $"{firstChar}{lastChar}";
        playerText.text = firstChar + lastChar;
        //if (System.Enum.TryParse(player.tag, out EnumPlayerTag parsedTag))
        //{
        //    playerTag = parsedTag;
        //    playerText.text = $"{player.tag}";
        //}
        //else
        //{
        //    //Debug.LogError($"Invalid player tag: {player.tag}");
        //}
    }

    //void FixedUpdate()
    //{
    //    if (playerTransform != null)
    //    {
    //        transform.position = playerTransform.position + textOffset;
    //    }
    //}

    public void DisplayPower(EnumPowerUp powerUp)
    {
        switch (powerUp)
        {
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

            default:
                DisableSprite();
                break;
        }
    }

    public void MultiBallSprite()
    {
        spriteRenderer.sprite = _multi;
        powerUpDisplayer.SetActive(true);
    }

    public void DisableSprite()
    {
        spriteRenderer.sprite = null;
        powerUpDisplayer.SetActive(false);
    }
}
