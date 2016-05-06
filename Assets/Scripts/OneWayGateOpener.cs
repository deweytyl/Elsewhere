using UnityEngine;
using System.Collections;

public class OneWayGateOpener : MonoBehaviour {

	public GameObject gateWall; 

	void OnTriggerEnter2D(Collider2D other) {
		gateWall.SetActive (false);
	}

	void OnTriggerExit2D(Collider2D other) {
		gateWall.SetActive (true);
	}
}
