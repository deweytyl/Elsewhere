using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	
	public GameObject respawnPoint;

	void Start() {
		if (!respawnPoint) {
			respawnPoint = GameObject.FindGameObjectWithTag ("Respawn");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameObject player = other.gameObject;

			player.GetComponent<PlayerControl> ().RespawnAt (respawnPoint.transform.position);
		
		} else if (other.gameObject.CompareTag ("Clone")) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");

			player.GetComponent<CloneAbility> ().DestroyClone ();
		
		}
	}
}
