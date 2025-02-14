using UnityEngine;
using TMPro;
public class PlayerText : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text playerText;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 textOffset = new Vector3 (0, 0, 3);
    //public EnumPlayerTag playerTag { get; private set; }
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

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + textOffset;
        }
    }
}
