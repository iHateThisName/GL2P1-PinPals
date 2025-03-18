using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsField : MonoBehaviour {
    //[SerializeField] private Transform spawnPoint;
    //[SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    //public List<Transform> _spawnPoints;
    [SerializeField] private PlayerJoinManager joinManager;
    private void OnTriggerEnter(Collider player) {
    joinManager.OnPlayerJoin();
        //other.transform.position = spawnPoint.position;
        //other.GetComponent<Rigidbody>().Sleep();
        //switch (GameManager.Instance.Players.Count)
        //{
        //    case 1:
        //        GameManager.Instance.MovePlayer(EnumPlayerTag.Player01, _spawnPoints[0].position);
        //        break;
        //    case 2:
        //        GameManager.Instance.MovePlayer(EnumPlayerTag.Player02, _spawnPoints[1].position);
        //        break;
        //    case 3:
        //        GameManager.Instance.MovePlayer(EnumPlayerTag.Player03, _spawnPoints[2].position);
        //        break;
        //    case 4:
        //        GameManager.Instance.MovePlayer(EnumPlayerTag.Player04, _spawnPoints[3].position);
        //        break;
        //}

    }
}
