using UnityEngine;
using System.Collections;

public class OneWayGateEntrance : MonoBehaviour {

	public BoxCollider2D gateCollider; 

	void OnTriggerEnter2D(Collider2D other) {
		OpenGate ();
	}

	void OnTriggerExit2D(Collider2D other) {
		CloseGate ();
	}

	void OpenGate() {
		gateCollider.enabled = false;
	}

	void CloseGate() {
		gateCollider.enabled = true;
	}
}
