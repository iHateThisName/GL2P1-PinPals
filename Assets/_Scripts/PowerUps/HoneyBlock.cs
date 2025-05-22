using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HoneyBlock : MonoBehaviour
{
    [SerializeField] private Vector3 _initialVelocity;
    private float _waitTime = 1f;
    private int _index = 0;
    private List<PlayerReferences> _players = new (); // Have the plater remember the initialvelocity in player references or have a reference to the player. or I can address the lists adjasently // if an object has alrady been detected, don't add a player to the lisyt already.
    private List<Vector3> _velocity = new();
    private List<EnumPlayerTag> _tags = new ();
    private void Start() {
        this.gameObject.SetActive(true);
    }
    public void OnTriggerEnter(Collider player) {
        SlowPlayer(player);
    }
    public void OnTriggerStay(Collider other) {
        SlowPlayer(other);
    }

    public void SlowPlayer(Collider player) {
        PlayerReferences playerReferences = player.GetComponent<PlayerReferences>();
        if (_tags.Contains(playerReferences.GetPlayerTag()))
            return;
        _tags.Add(playerReferences.GetPlayerTag());
        _velocity.Add(player.GetComponent<Rigidbody>().linearVelocity);
        _players.Add(playerReferences);
        //Debug.Log("Honey On" + player.gameObject.name);
        if (player.gameObject.name.Contains("Clone"))
        {
            //player.gameObject.GetComponent<Rigidbody>().linearDamping = 100.0f;
            //player.gameObject.GetComponent<Rigidbody>().angularDamping = 100f;
            player.gameObject.GetComponent<PhysicsMaterial>().dynamicFriction = 100f;
        }
        // Detects the players and de-accelerates their speed.

        if (player.gameObject.tag.StartsWith("Player"))
        {
            //Debug.Log("Honey On" + player.gameObject.name);
            EnumPlayerTag tag = playerReferences.GetPlayerTag();
            //player.gameObject.GetComponent<Collider>().material.dynamicFriction = 100f;
            playerReferences.rb.linearDamping = 25f;
            _initialVelocity = playerReferences.rb.linearVelocity;
            playerReferences.rb.linearVelocity = _initialVelocity * .5f;
            StartCoroutine(DestroyGameObject(_waitTime));
        }
    }
    public void OnTriggerExit(Collider player) {
        //Debug.Log("Honey Off" + player.gameObject.name);
        PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
        playerRef.rb.linearDamping = 0;
    }
    public IEnumerator DestroyGameObject(float waitTime) {
        yield return new WaitForSecondsRealtime(waitTime);
        ResetPlayerSpeed();
        this.gameObject.SetActive(false);
    }

    public void ResetPlayerSpeed() {
        foreach (PlayerReferences player in _players) {
            foreach (Vector3 velocity in _velocity) {
            _index = _velocity.IndexOf(velocity);
            }
            player.rb.linearDamping = 0;
        }
        _players.Clear();
        _velocity.Clear();
    }
}
