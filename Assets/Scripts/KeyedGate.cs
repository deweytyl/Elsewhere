using UnityEngine;
using System.Collections;

public class KeyedGate : MonoBehaviour {

	public GameObject gateRoot;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Key")) {
			other.GetComponent<Key> ().Destroy ();
			Destroy (gateRoot);
		}
	}
}
