using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private Vector3 originalPosition;
	private GameObject keyCarrier;

	// Public methods

	public void Drop() {
		RemoveFromCarrier ();
		transform.position = GridUtil.RoundToNearestHalf (transform.position);
	}

	public void Reset() {
		RemoveFromCarrier ();
		transform.position = originalPosition;
	}

	public void Destroy() {
		Destroy (gameObject);
	}

	// Unity Lifecycle methods

	void Start() {
		originalPosition = transform.position;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Clone")) {
			keyCarrier = other.gameObject;
			FollowCarrier ();
			ResizeKey ();
		}
	}

	// Helper methods

	void FollowCarrier() {
		transform.parent = keyCarrier.transform;
		transform.localPosition = Vector3.zero;
	}

	void ResizeKey () {
		transform.localScale = new Vector3 (0.75f, 0.75f);
	}

	void RemoveFromCarrier() {
		transform.parent = null;
		keyCarrier = null;
	}
		
}
