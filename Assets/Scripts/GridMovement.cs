using UnityEngine;
using System.Collections;

// Based on http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html

public class GridMovement : MonoBehaviour {

	public float speed = 2.0f;

	private int steps = 0;
	private bool isMoving = false;
	private Vector3 destination;
	private Vector3 previousPosition;

	// Use this for initialization
	void Start() {
		ClearDestination ();
	}

	public void ClearDestination() {
		destination = transform.position;
	}

	// Update is called once per frame
	void Update() {
		PerformMove ();
	}

//	void OnCollisionEnter2D(Collision2D collision) {
//		ResetDestination ();
//	}

	public int Steps() {
		return steps;
	}

	public bool IsMoving() {
		return isMoving;
	}

	void PerformMove () {
		if (transform.position == destination) {
			isMoving = false;
			steps++;
			previousPosition = transform.position;

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

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			destination += Vector3.down;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			destination += Vector3.left;

		} else if (Input.GetKey (KeyCode.RightArrow)) {
			destination += Vector3.right;
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
		destination = previousPosition;
	}
}
