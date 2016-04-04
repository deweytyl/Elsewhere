using UnityEngine;
using System.Collections;

// Based on http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html

public class GridMovement : MonoBehaviour {

	public float speed = 2.0f;

	private Vector3 destination;
	private Vector3 previousPosition;

	// Use this for initialization
	void Start () {
		destination = transform.position;
	}

	// Update is called once per frame
	void Update () {
		PerformMove ();
	}

//	void OnCollisionEnter2D(Collision2D collision) {
//		ResetDestination ();
//	}

	void PerformMove () {
		if (transform.position == destination) {
			//if (previousPosition != destination)
			//	Debug.LogFormat (string.Format ("previousPosition = {0}/ndestination = {1}", previousPosition, destination));

			previousPosition = destination;

			UpdateDestinationForInputDirection ();
		}

		transform.position = Vector3.MoveTowards (transform.position, destination, Time.deltaTime * speed);
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
			destinationIsWall = destinationIsWall || !(collider.isTrigger);
		}

		return destinationIsWall;
	}

	void ResetDestination() {
		destination = previousPosition;
	}
}
