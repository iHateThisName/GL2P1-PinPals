using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class HoneyBlock : MonoBehaviour {
    private GameObject _honeyOwner;
    private float _waitTime = 5f;
    public void AssignOwner(GameObject owner) {
        this._honeyOwner = owner;
    }
    private void Start() {
        this.gameObject.SetActive(true);
    }
    public void OnCollisionEnter(Collision player) {

        if (player.gameObject.name.Contains("Clone"))
        {
            player.gameObject.GetComponent<Rigidbody>().linearDamping = 10.0f;
        }
        // Detects the players and de-accelerates their speed.
        if (player.gameObject != _honeyOwner)
        {
            if (player.gameObject.tag.StartsWith("Player"))
            {
                PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
                EnumPlayerTag tag = playerRef.GetPlayerTag();
                playerRef.rb.linearDamping = 10.0f;
            }
        }
        StartCoroutine(DestroyGameObject(_waitTime));
        Destroy(this.gameObject);
    }
    public void OnCollisionExit(Collision player) {
        PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
        playerRef.rb.angularDamping = 0.0f;
        playerRef.rb.linearDamping = 0.0f;
    }
    public IEnumerator DestroyGameObject(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
