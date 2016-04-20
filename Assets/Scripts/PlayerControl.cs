using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public bool moveble = false;

	private GridMovement movement;
	private GridMovement cloneMovement;
	private CloneAbility cloneAbility;
	private Animator animator;

	private GameObject clone;

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
			}
		} else if (movement.enabled) {
			movement.MoveInDirection (direction);
		}
	}

	public void RespawnAt(Vector3 respawnPoint) {
		animator.SetTrigger ("respawn");

		// must be called after resetting player.transform.position
		// perhaps something to fix in the future?
		gameObject.transform.position = respawnPoint;
		movement.ClearDestination ();
	}

}
