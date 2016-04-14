using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	
	public GameObject respawnPoint;

	private bool containsSpawner;
	private GameObject cloneSpawner;

	void Update() {
		// Relies on fact clone spawner is destroyed when clone spawns
		if (containsSpawner && cloneSpawner == null) {
			GameObject clone = GameObject.FindGameObjectWithTag ("Clone");

			Destroy (clone); // assumes only one clone

			containsSpawner = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameObject player = other.gameObject;

			player.transform.position = respawnPoint.transform.position;

			// must be called after resetting player.transform.position
			// something to fix in the future
			GridMovement movement = player.GetComponent<GridMovement> ();
			movement.ClearDestination ();
		
		} else if (other.gameObject.CompareTag ("Clone")) {
			Destroy (other.gameObject);
		
		} else if (other.gameObject.CompareTag ("CloneSpawner")) {
			cloneSpawner = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("CloneSpawner")) {
			containsSpawner = false;
			cloneSpawner = null;
		}
	}
}
