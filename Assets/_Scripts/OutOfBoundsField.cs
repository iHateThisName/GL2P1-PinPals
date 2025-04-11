using UnityEngine;

public class OutOfBoundsField : MonoBehaviour {
    private int _point = 1;
    //[SerializeField] private Transform spawnPoint;
    //[SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    //public List<Transform> _spawnPoints;
    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.name.Contains("Clone"))
        {
            Destroy(player.gameObject);
            return;
        }

        PlayerReferences controller = player.gameObject.GetComponent<PlayerReferences>();
            EnumPlayerTag tag = controller.GetPlayerTag();
            PlayerPowerController power = controller.PlayerPowerController;
            player.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerDeaths(_point);
            //Remove powerups before respawning
            power.RemoveCurrentPower();


            PlayerJoinManager.Instance.Respawn(tag);


    }
}
