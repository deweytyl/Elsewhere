using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	
	public GameObject respawnPoint;

	void Update() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			GameObject player = other.gameObject;

			player.transform.position = respawnPoint.transform.position;

			// must be called after resetting player.transform.position
			// something to fix in the future
			GridMovement movement = player.GetComponent<GridMovement> ();
			movement.ClearDestination ();
		
		} else if (other.gameObject.name == "Clone") {
			Destroy (other.gameObject);
		
		} else if (other.gameObject.name == "CloneSpawner") {

		}
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
