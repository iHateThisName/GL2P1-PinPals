using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class PlayerText : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text playerText;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 textOffset = new Vector3 (0, 0, 3);
    [SerializeField] public GameObject multiBallUI;
    //[SerializeField] private EnumPowerUp powerUps;
    [SerializeField] private Sprite _multi, _shrink, _grow;
    //public EnumPlayerTag playerTag { get; private set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        playerText.text = $"{player.tag}";
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

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + textOffset;
        }
    }

    public void DisplayPower(EnumPowerUp powerUp)
    {
        switch (powerUp)
        {
            case EnumPowerUp.None:
                DisableSprite();
                break;

            case EnumPowerUp.Shrink:
                spriteRenderer.sprite = _shrink;
                multiBallUI.SetActive(true);
                break;

            case EnumPowerUp.Grow:
                spriteRenderer.sprite = _grow;
                multiBallUI.SetActive(true);
                break;

            case EnumPowerUp.MultiBall:
                spriteRenderer.sprite = _multi;
                multiBallUI.SetActive(true);
                break;
            default:
                DisableSprite();
                break;
        }
    }

    public void MultiBallSprite()
    {
        spriteRenderer.sprite = _multi;
        multiBallUI.SetActive(true);
    }

    public void DisableSprite()
    {
        spriteRenderer.sprite = null;
        multiBallUI.SetActive(false);
    }
}
