using UnityEngine;

public class HoneyBlock : MonoBehaviour {
    private GameObject _honeyOwner;
    public void AssignOwner(GameObject owner) {
        this._honeyOwner = owner;
    }
    private void Start() {
        this.gameObject.SetActive(true);
    }
    //public async void OnTriggerEnter(Collider player) {
    //    if (player.gameObject.name.Contains("Clone")) {
    //        player.GetComponent<Rigidbody>().linearDamping = 5.0f;
    //    }
    //    // Detect every player in the collider then respawn them
    //    if (player.gameObject != _honeyOwner) {
    //        if (player.gameObject.tag.StartsWith("Player")) {
    //            EnumPlayerTag tag = player.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
    //            player.GetComponent<Rigidbody>().linearDamping = 5.0f;
    //        }
    //    }
    //    if (player.gameObject.tag == ("Bumper")) {
    //        Destroy(player.gameObject);
    //    }
    //    await Task.Delay(3000);
    //    Destroy(this.gameObject);
    //}

    public void OnTriggerExit(Collider player) {
        player.GetComponent<Rigidbody>().angularDamping = 0f;
        player.GetComponent<Rigidbody>().linearDamping = 0f;
    }
}
