using UnityEngine;
using System.Collections;

public class CrumblePanel : Trigger {

	private bool active = true;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Ouch, i was stepped on");

		if (active) {
			EnableListenerTriggers ();
			active = false;
		}
	}
}
