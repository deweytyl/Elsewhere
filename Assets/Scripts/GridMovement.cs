﻿using UnityEngine;
using System.Collections;
using System.Linq;

// Based on http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html

public class GridMovement : MonoBehaviour {

	public bool selfControlling = false;
	public float speed = 2.5f;

	public bool isOnIce = false;

	private const float COLLISION_RADIUS = 0.25f;

	private float originalSpeed;
	private int steps = 0;
	private bool isMoving = false;

	private Vector3 direction = Vector3.zero;
	private Vector3 destination;
	private Vector3 previousPosition;

	private GameObject player;
	static Quaternion playerRotation;

	// Use this for initialization
	void Start() {
		ClearDestination ();
		playerRotation = new Quaternion();
		originalSpeed = speed;
		player = GameObject.Find ("Player");
	}

	void Update() {
		player.transform.rotation = rotatePlayer ();
		PerformMove ();
	}

	static Quaternion rotatePlayer () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		}else if (Input.GetKey (KeyCode.LeftArrow)) {
			GridMovement.playerRotation.Set(0.0f, 180.0f, 0.0f, 0.0f);
		}
		return GridMovement.playerRotation;
	}

	void PerformMove () {
		if (transform.position == destination) {
			if (previousPosition != transform.position) {
				previousPosition = transform.position;
				steps++;
			}

			isMoving = false;

			if (selfControlling) {
				UpdateDestinationForInputDirection ();
			}
		}

		if (destination != transform.position) {
			transform.position = Vector3.MoveTowards (transform.position, destination, Time.deltaTime * speed);
			isMoving = true;
		}
	}

	void UpdateDestinationForInputDirection() {
		direction = GetDirectionFromInput ();

		MoveInDirection (direction);
	}

	public void MoveInDirection (Vector3 newDirection) {
		if (!isMoving) {
			direction = newDirection;

			IncrementDestinationByCurrentDirection ();
		}
	}

	public void IncrementDestinationByCurrentDirection() {
		if (!CheckForObstacles (destination + direction, direction)) {
			destination += direction;
		}
	}

	public void ClearDestination() {
		destination = gameObject.transform.position;
		previousPosition = destination;
	}

	public void ResetSpeed() {
		speed = originalSpeed;
	}

	bool CheckForObstacles (Vector3 newDestination, Vector3 newDirection) {
		if (CompareTag ("Player") || CompareTag ("Clone")) {
			return CheckCloneOrPlayerObstacle (newDestination, newDirection);
		} else {
			return IsObstacle (newDestination);
		}
	}

	bool CheckCloneOrPlayerObstacle (Vector3 newDestination, Vector3 newDirection) {
		Collider2D[] colliders = GetPossibleObstacles (newDestination);
		bool obstacleIsCloneOrPlayer = colliders.Any (collider => {
			return collider.gameObject.CompareTag ("Player") ||
				collider.gameObject.CompareTag ("Clone");
		});

		// if the obstacle is a clone or the player, 
		// check for obstacles one beyond
		if (obstacleIsCloneOrPlayer) {
			return IsObstacle (newDestination + newDirection);
		} else {
			return IsObstacle (newDestination);
		}
	}

	public static Vector3 GetDirectionFromInput() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			return Vector3.up;

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			
			return Vector3.down;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			return Vector3.left;

		} else if (Input.GetKey (KeyCode.RightArrow)) {
			return Vector3.right;

		} else {
			return Vector3.zero;
		}
	}

	public static bool IsObstacle(Vector3 position) {
		Collider2D[] colliders = GetPossibleObstacles (position);

		return colliders.Any (collider => {
			return !collider.isTrigger &&
				!collider.gameObject.CompareTag ("Player") &&
				!collider.gameObject.CompareTag ("Clone");
		});
	}

	static Collider2D[] GetPossibleObstacles(Vector3 position) {
		return Physics2D.OverlapCircleAll (position, COLLISION_RADIUS);
	}


	// Property accessors

	public int Steps() {
		return steps;
	}

	public bool IsMoving() {
		return isMoving;
	}

	public Quaternion getRotation() {
		return playerRotation;
	}

	public Vector3 Direction() {
		return direction;
	}
}
