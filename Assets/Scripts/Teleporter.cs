using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Teleporter destination;

	private bool shouldTeleport = true;

	public void PrepareForLanding() {
		shouldTeleport = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (shouldTeleport && 
			(other.CompareTag ("Player") || other.CompareTag ("Clone"))) {
			destination.PrepareForLanding ();
			other.transform.position = destination.transform.position;
			other.GetComponent<GridMovement> ().ClearDestination ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		shouldTeleport = true;
	}

}
