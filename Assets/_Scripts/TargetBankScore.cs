using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetBankScore : MonoBehaviour
{
    private int _maxHits = 0;
    private int _currentHits = 0;
    [SerializeField] private PlayerScoreTracker tracker;
    [SerializeField] int points = 500;

    //[SerializeField] private ScoreManager scoreManager;

    void Start()
    {
        _maxHits = transform.GetComponentsInChildren<OnOffThingieScript>().Length;
    }

    public void OnScore(PlayerScoreTracker playerTracker)
    {
        _currentHits++;

        if (_currentHits == _maxHits)
        {
            playerTracker.AddPoints(points);
        }
    }
}
