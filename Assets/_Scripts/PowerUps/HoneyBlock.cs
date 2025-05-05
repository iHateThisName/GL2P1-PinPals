using System.Collections;
using UnityEngine;

public class HoneyBlock : MonoBehaviour
{
    private GameObject _honeyOwner;
    private float _waitTime = 5f;
    /*public void AssignOwner(GameObject owner) {
        this._honeyOwner = owner;
    }*/
    private void Start() {
        this.gameObject.SetActive(true);
    }
    public void OnCollisionStay(Collision player) {

        if (player.gameObject.name.Contains("Clone"))
        {
            //this.gameObject.GetComponent<PhysicsMaterial>().staticFriction = 10f;
            this.gameObject.GetComponent<PhysicsMaterial>().dynamicFriction = 10f;
            player.gameObject.GetComponent<Rigidbody>().linearDamping = 10.0f;
            player.gameObject.GetComponent<Rigidbody>().angularDamping = 100f;
        }
        // Detects the players and de-accelerates their speed.

        if (player.gameObject.tag.StartsWith("Player"))
        {
            PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
            EnumPlayerTag tag = playerRef.GetPlayerTag();
            this.gameObject.GetComponent<PhysicsMaterial>().dynamicFriction = 100f;
            playerRef.rb.linearDamping = 10;
            playerRef.rb.angularDamping = 100;
        }

        StartCoroutine(DestroyGameObject(_waitTime));
        Destroy(this.gameObject);
    }
    public void OnCollisionExit(Collision player) {
        PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
        playerRef.rb.angularDamping = 0.0f;
        playerRef.rb.linearDamping = 0.0f;
        this.gameObject.GetComponent<PhysicsMaterial>().dynamicFriction = 1f;
    }
    public IEnumerator DestroyGameObject(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
