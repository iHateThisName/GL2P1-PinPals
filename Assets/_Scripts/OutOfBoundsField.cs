using UnityEngine;

public class OutOfBoundsField : MonoBehaviour {
    //[SerializeField] private Transform spawnPoint;
    //[SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    //public List<Transform> _spawnPoints;
    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.name.Contains("Clone"))
        {
            Destroy(player.gameObject);
            return;
        }

        ModelController controller = player.gameObject.GetComponent<ModelController>();
            EnumPlayerTag tag = controller.GetPlayerTag();
            PlayerPowerController power = controller.PlayerPowerController;
            //Remove powerups before respawning
            power.RemoveCurrentPower();


            PlayerJoinManager.Instance.Respawn(tag);


    }
}
