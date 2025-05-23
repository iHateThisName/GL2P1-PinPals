using System.Collections;
using TMPro;
using UnityEngine;

// Ivar
public class PlayerScoreTracker : MonoBehaviour {
    public int currentScore { get; private set; } = 0;
    private int _animatedScore = 0;
    private bool _isScoreAnimating = false;
    [SerializeField] private float _animationSpeed = 5.0f;
    public EnumPlayerTag playerTag { get; private set; }

    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text _scoreText;

    private void Start() {
        if (System.Enum.TryParse(player.tag, out EnumPlayerTag parsedTag)) {
            playerTag = parsedTag;
        } else {
            Debug.LogError($"Invalid player tag: {player.tag}");
        }
    }

    public void AddPoints(int points) {
        currentScore += points;
        if (!_isScoreAnimating) {
            StartCoroutine(AnimateScore());
        }
        HighScore.Instance.CheckHighScore(currentScore);
    }

    public void DockPoints(int points) {
        currentScore -= points;
        if (!_isScoreAnimating)
        {
            StartCoroutine(AnimateDockedScore());
        }
        HighScore.Instance.CheckHighScore(currentScore);
    }


    private IEnumerator AnimateScore() {
        _isScoreAnimating = true;
        while (_animatedScore < currentScore) {
            _animatedScore = (int)Mathf.MoveTowards(_animatedScore, currentScore, _animationSpeed * Time.deltaTime * 100);
            _scoreText.text = $"Score: {_animatedScore}";
            yield return null;
        }
        _isScoreAnimating = false;
    }

    private IEnumerator AnimateDockedScore() {
        _isScoreAnimating = true;
        while (_animatedScore > currentScore) {
            _animatedScore = (int)Mathf.MoveTowards(_animatedScore, currentScore, _animationSpeed * Time.deltaTime * 100);
            _scoreText.text = $"Score: {_animatedScore}";
            yield return null;
        }
        _isScoreAnimating = false;
    }
}
