using UnityEngine;

public class ModelController : MonoBehaviour
{
[SerializeField] private PlayerScoreTracker _playerScoreTracker;
    public PlayerPowerController PlayerPowerController;

    public void AddPlayerPoints(int points)
    {
        _playerScoreTracker.AddPoints(points);
    }
}
