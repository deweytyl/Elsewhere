using UnityEngine;
using System.Collections;

public class DoubleCrumblePanels : Trigger {

	private Animator animator;
	private bool active = true;

	public DoubleCrumblePanels pair;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (active && pair.IsSteppedOn ()) {
			EnableListenerTriggers ();
			Crumble ();
			pair.Crumble ();
		}
	}

	public void Crumble() {
		active = false;
		animator.SetBool ("active", false);
	}

	public bool IsSteppedOn () {
		Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll (transform.position, 0.4f);

		foreach (Collider2D overlapper in overlappingColliders) {
			if (overlapper.gameObject != gameObject) {
				return true;
			}
		}

		return false;
	}

}
