using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameController : MonoBehaviour {

    [Header("Pillers")]
    [SerializeField] private GameObject simpleBall;
    [SerializeField] private Transform FirstPlacePiller, SecondPlacePiller, ThirdPlacePiller;
    [SerializeField] private TMP_Text FirstPlaceScore, SecondPlaceScore, ThirdPlaceScore;

    [Header("ScoreBoard")]
    [SerializeField] private GameObject _scoreUIPrefab, _scoreBoard;


    private List<(EnumPlayerTag tag, int score)> playerScores;
    private List<(EnumPlayerTag tag, Color color)> playerColors = new List<(EnumPlayerTag tag, Color color)>();

    private void Awake() {
        PlayerSettings.IsLandscape = false;
    }

    private void Start() {
        if (FirstPlacePiller == null || SecondPlacePiller == null || ThirdPlacePiller == null) {
            Debug.LogError("One or more of the place pillers are not assigned in the inspector.");
            return;
        }

        if (FirstPlaceScore == null || SecondPlaceScore == null || ThirdPlaceScore == null) {
            Debug.LogError("One or more of the place score texts are not assigned in the inspector.");
            return;
        }

        this.playerScores = GameManager.Instance.GetPlayersOrderByScoreWithScore();

        // Remove all players except the top 3
        for (int i = 0; i < this.playerScores.Count; i++) {
            EnumPlayerTag tag = this.playerScores[i].tag;
            ModelController modelController = GameManager.Instance.GetModelController(tag);
            Color color = modelController.SkinController.GetColor();
            this.playerColors.Add((tag: tag, color: color));
            if (i < 3) {
                if (i == 0) {
                    GameObject simplePlayer = Instantiate(simpleBall, FirstPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = modelController.SkinController.GetMaterial();
                } else if (i == 1) {
                    GameObject simplePlayer = Instantiate(simpleBall, SecondPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = modelController.SkinController.GetMaterial();
                } else if (i == 2) {
                    GameObject simplePlayer = Instantiate(simpleBall, ThirdPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = modelController.SkinController.GetMaterial();
                }
            } else {
                //GameManager.Instance.DeletePlayer(tag);
            }
            CreateScoreboardElement(i);
        }
        //GameManager.Instance.DeleteAllPlayers()/*;*/
        GameManager.Instance.HidePlayers();

        if (this.playerScores.Count == 0) return;
        this.FirstPlaceScore.text = this.playerScores[0].score.ToString();
        if (this.playerScores.Count > 1) this.SecondPlaceScore.text = this.playerScores[1].score.ToString();
        if (this.playerScores.Count > 2) this.ThirdPlaceScore.text = this.playerScores[2].score.ToString();
    }

    private void CreateScoreboardElement(int index) {
        // Create the scoreboard element
        GameObject playerScoreBoard = Instantiate(_scoreUIPrefab, Vector3.zero, Quaternion.identity);
        playerScoreBoard.transform.SetParent(this._scoreBoard.transform, false);

        // This create a string like P01, P02, P03
        string playerString = "Player " + playerScores[index].tag.ToString().Substring(playerScores[index].tag.ToString().Length - 2);

        // Stats gets added to the ui
        ScoreUIController scoreUIController = playerScoreBoard.GetComponent<ScoreUIController>();
        scoreUIController.SetUIText(playerString + " - " + playerScores[index].score.ToString());
        scoreUIController.SetColor(this.playerColors[index].color);
    }
}
