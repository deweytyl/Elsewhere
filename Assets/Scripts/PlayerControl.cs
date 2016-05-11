using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public bool moveble = false;
	public float respawnMovementDelay = 0.5f;

	private GridMovement movement;
	private GridMovement cloneMovement;
	private CloneAbility cloneAbility;
	private Animator animator;

	private GameObject clone;
	private Key key;

	void Start() {
		movement = GetComponent<GridMovement> ();
		cloneAbility = GetComponent<CloneAbility> ();
		animator = GetComponent<Animator> ();
	}

	void LateUpdate() {
		Vector3 direction = GridMovement.GetDirectionFromInput ();

		if ((clone = cloneAbility.GetClone ())) { // intentional assignment

			cloneMovement = clone.GetComponent<GridMovement> ();

			if (!movement.IsMoving () && !cloneMovement.IsMoving ()) {
				movement.MoveInDirection (direction);
				cloneMovement.MoveInDirection (direction);

			} else if (cloneMovement.isOnIce && !movement.IsMoving ()) {
				movement.MoveInDirection (direction);

			} else if (movement.isOnIce && !cloneMovement.IsMoving ()) {
				cloneMovement.MoveInDirection (direction);
			}
		} else if (movement.enabled) {
			movement.MoveInDirection (direction);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Key")) {
			key = other.GetComponentInChildren<Key> ();
		}
	}

	public void RespawnAt(Vector3 respawnPoint) {
		animator.SetTrigger ("respawn");

		if (key) {
			key.Reset ();
		}
		
		// must be called after resetting player.transform.position
		// perhaps something to fix in the future?
		gameObject.transform.position = respawnPoint;
		movement.ClearDestination ();
		movement.enabled = false;

		StartCoroutine (EnableMovementAfterDelay (respawnMovementDelay));
	}

	public IEnumerator EnableMovementAfterDelay(float delay) {
		yield return new WaitForSeconds (delay);

		movement.enabled = true;
	}

}
