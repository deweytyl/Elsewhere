using UnityEngine;
using System.Collections;

public class PressurePlate : Trigger {

	void OnTriggerEnter2D(Collider2D other) {
		EnableListenerTriggers ();
	}

	void OnTriggerExit2D(Collider2D other) {
		DisableListenerTriggers ();
	}

}
