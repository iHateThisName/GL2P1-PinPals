using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeTargetsScore : MonoBehaviour
{
    private int _maxHits = 0;
    private int _currentHits = 0;

    [SerializeField] private int _bonusPoints = 500;

    [SerializeField] private ScoreManager scoreManager;
 
    void Start()
    {
        _maxHits = transform.GetComponentsInChildren<OnOffThingieScript>().Length;
    }

    public void OnScore()
    {
        _currentHits++;

        if (_currentHits == _maxHits)
        {
            scoreManager.score += _bonusPoints;
        }
    }
}
