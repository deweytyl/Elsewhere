using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

	//public Texture2D source;
	public GameObject source;

	public void Start() {
		source.SetActive (false);
	}

	public void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "Char") {
			source.SetActive (true);
		}
	}

}
