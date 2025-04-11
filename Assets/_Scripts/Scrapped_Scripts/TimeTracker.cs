using TMPro;
using UnityEngine;

public class TimeTracker : MonoBehaviour {
    [SerializeField] private TMP_Text _timeText;
    TimeManager timeManager;
    private void LateUpdate() {
        _timeText.text = TimeManager.Instance.TimerText;
    }
}
