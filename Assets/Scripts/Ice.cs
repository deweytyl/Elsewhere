using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	private const float ICE_SPEED = 4.0f;

	private GridMovement presentMovement;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Clone")) {
			presentMovement = other.GetComponent<GridMovement> ();

			presentMovement.IncrementDestinationByCurrentDirection ();
		}
	}

	IEnumerator OnTriggerStay2D(Collider2D other) {
		// wait until one ice panel
		yield return new WaitWhile (() => presentMovement && presentMovement.isOnIce);

		if (presentMovement) {
			presentMovement.isOnIce = true;
			presentMovement.speed = ICE_SPEED;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (presentMovement) {
			presentMovement.isOnIce = false;
			presentMovement.ResetSpeed ();
			presentMovement = null;
		}
	}
}
