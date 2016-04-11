using UnityEngine;
using System.Collections;

// Based on http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html

public class GridMovement : MonoBehaviour {

	public float speed = 2.0f;

	private int steps = 0;
	private bool isMoving = false;
	private Vector3 direction = Vector3.right;

	private Vector3 destination;
	private Vector3 previousPosition;

	// Use this for initialization
	void Start() {
		ClearDestination ();
	}

	public void ClearDestination() {
		destination = transform.position;
		previousPosition = destination;
	}

	void Update() {
		PerformMove ();
	}

	public int Steps() {
		return steps;
	}

	public bool IsMoving() {
		return isMoving;
	}

	public Vector3 Direction() {
		return direction;
	}

	void PerformMove () {
		if (transform.position == destination) {
			if (previousPosition != transform.position) {
				previousPosition = transform.position;
				steps++;
			}

			isMoving = false;

			UpdateDestinationForInputDirection ();
		}

		if (destination != transform.position) {
			transform.position = Vector3.MoveTowards (transform.position, destination, Time.deltaTime * speed);
			isMoving = true;
		}
	}

	void UpdateDestinationForInputDirection() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			destination += Vector3.up;
			direction = Vector3.up;

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			destination += Vector3.down;
			direction = Vector3.down;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			destination += Vector3.left;
			direction = Vector3.left;

		} else if (Input.GetKey (KeyCode.RightArrow)) {
			destination += Vector3.right;
			direction = Vector3.right;
		}
			
		if (IsWall (destination)) {
			ResetDestination ();
		}

	}

	bool IsWall(Vector3 position) {
		bool destinationIsWall = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll (destination, 0.1f);

		foreach (Collider2D collider in colliders) {
			destinationIsWall = destinationIsWall || !collider.isTrigger;
		}
		return destinationIsWall;
	}

	void ResetDestination() {
		direction = previousPosition - destination;
		destination = previousPosition;
	}
}
