using UnityEngine;
using System.Collections;

public class RevealOnTrigger : MonoBehaviour {
	void EnableTrigger() {
		Debug.Log ("Was enabled!!");
		GetComponent<SpriteRenderer>().enabled = true;
	}

	void DisableTrigger() {
		Debug.Log ("Was disabled!");
		GetComponent<SpriteRenderer>().enabled = false;
	}

}