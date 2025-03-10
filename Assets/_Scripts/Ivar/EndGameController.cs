using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameController : MonoBehaviour {

    [SerializeField] private Transform FirstPlacePiller, SecondPlacePiller, ThirdPlacePiller;
    [SerializeField] private TMP_Text FirstPlaceScore, SecondPlaceScore, ThirdPlaceScore;

    [SerializeField] private GameObject simpleBall;

    private List<(EnumPlayerTag tag, int score)> playerScores;

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
            //GameManager.Instance.GetPlayerController(this.playerScores[i].tag).DisableGravity();
            if (i < 3) {
                //GameManager.Instance.GetPlayerController(this.playerScores[i].tag).DisableGravity();
                //GameManager.Instance.GetModelController(this.playerScores[i].tag).rb.MovePosition(Vector3.zero);
                //GameManager.Instance.GetModelController(this.playerScores[i].tag).rb.Move(FirstPlacePiller.position, Quaternion.identity);


                //if (i == 0) GameManager.Instance.MovePlayer(this.playerScores[i].tag, newPosition: FirstPlacePiller.position, false);
                //if (i == 1) GameManager.Instance.MovePlayer(this.playerScores[i].tag, SecondPlacePiller.position, false);
                //if (i == 2) GameManager.Instance.MovePlayer(this.playerScores[i].tag, ThirdPlacePiller.position, false);
                if (i == 0) {
                    GameObject simplePlayer = Instantiate(simpleBall, FirstPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = GameManager.Instance.GetModelController(this.playerScores[i].tag).gameObject.GetComponent<Renderer>().material;
                } else if (i == 1) {
                    GameObject simplePlayer = Instantiate(simpleBall, SecondPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = GameManager.Instance.GetModelController(this.playerScores[i].tag).gameObject.GetComponent<Renderer>().material;
                } else if (i == 2) {
                    GameObject simplePlayer = Instantiate(simpleBall, ThirdPlacePiller.position, Quaternion.identity);
                    simplePlayer.GetComponent<Renderer>().material = GameManager.Instance.GetModelController(this.playerScores[i].tag).gameObject.GetComponent<Renderer>().material;
                }


            } else {
                GameManager.Instance.DeletePlayer(this.playerScores[i].tag);
            }
        }
        GameManager.Instance.DeleteAllPlayers();

        if (this.playerScores.Count == 0) return;
        this.FirstPlaceScore.text = this.playerScores[0].score.ToString();
        if (this.playerScores.Count > 1) this.SecondPlaceScore.text = this.playerScores[1].score.ToString();
        if (this.playerScores.Count > 2) this.ThirdPlaceScore.text = this.playerScores[2].score.ToString();

        //GameManager.Instance.CheckCamera();
    }

    //private void FixedUpdate() {
    //    this.playerScores = GameManager.Instance.GetPlayersOrderByScoreWithScore();

    //    // Remove all players except the top 3
    //    for (int i = 0; i < this.playerScores.Count; i++) {

    //        if (i < 3) {
    //            if (i == 0) GameManager.Instance.MovePlayer(this.playerScores[i].tag, newPosition: FirstPlacePiller.position, false);
    //            if (i == 1) GameManager.Instance.MovePlayer(this.playerScores[i].tag, SecondPlacePiller.position, false);
    //            if (i == 2) GameManager.Instance.MovePlayer(this.playerScores[i].tag, ThirdPlacePiller.position, false);
    //            Debug.Log("World Position: " + FirstPlacePiller.position);
    //            GameManager.Instance.GetPlayerController(this.playerScores[i].tag).DisableGravity();

    //        } else {
    //            GameManager.Instance.DeletePlayer(this.playerScores[i].tag);
    //        }
    //    }

    //    if (this.playerScores.Count == 0) return;
    //    this.FirstPlaceScore.text = this.playerScores[0].score.ToString();
    //    if (this.playerScores.Count > 1) this.SecondPlaceScore.text = this.playerScores[1].score.ToString();
    //    if (this.playerScores.Count > 2) this.ThirdPlaceScore.text = this.playerScores[2].score.ToString();

    //    GameManager.Instance.CheckCamera();
    //}

}
