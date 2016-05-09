using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	private GridMovement presentMovement;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Clone")) {
			presentMovement = other.GetComponent<GridMovement> ();
			presentMovement.IncrementDestinationByCurrentDirection ();
		}
	}

	IEnumerator OnTriggerStay2D(Collider2D other) {
		// wait until one ice panel
		yield return new WaitWhile (() => presentMovement.isOnIce);
		presentMovement.isOnIce = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		presentMovement.isOnIce = false;
		presentMovement = null;
	}
}
