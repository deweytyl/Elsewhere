using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	public float x;
	public float y;
	public bool active = true;
	void OnTriggerEnter2D (Collider2D other) {
		if (active) {
			if (other.gameObject.name == "Player") {
				GameObject player = other.gameObject;

				player.transform.position = new Vector3 (x, y, 0);
				GridMovement movement = player.GetComponent<GridMovement> ();
				movement.ClearDestination ();
			}

			if (other.gameObject.name == "Clone") {
				Destroy (other.gameObject);
			}
		}
	}
}
