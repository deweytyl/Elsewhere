using UnityEngine;
using System.Collections;

public class FakeFloor : MonoBehaviour {

	public Hole hole;

	void Start () {
		hole.active = false;
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player" || other.name == "Clone") {
			hole.active = true;
			gameObject.SetActive (false);
		}
	}

}
