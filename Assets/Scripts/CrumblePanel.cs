using UnityEngine;
using System.Collections;

public class CrumblePanel : Trigger {

	private Animator animator;
	private bool active = true;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Ouch, i was stepped on");

		if (active) {
			EnableListenerTriggers ();
			active = false;
			animator.SetBool ("active", false);
		}
	}
}
