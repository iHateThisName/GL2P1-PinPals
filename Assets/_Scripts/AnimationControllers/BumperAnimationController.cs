using UnityEngine;

public class BumperAnimationController : MonoBehaviour {
    [SerializeField] private GameObject _animationRipplePrefab;
    [SerializeField] private GameObject _animationScorePrefab;
    [SerializeField] private string ScoreText = "+100";

    public void PlayAnimation(Collision collision) {
        GameObject animationRipple = Instantiate(_animationRipplePrefab, collision.contacts[0].point, Quaternion.identity);
        GameObject animationScore = Instantiate(_animationScorePrefab, this.gameObject.transform.position + new Vector3(0, 7, 0), Quaternion.identity);
        animationScore.GetComponent<ScoreAnimationController>().AssaigneScoreText(this.ScoreText);
        Destroy(animationRipple, 1.25f);
    }
}
