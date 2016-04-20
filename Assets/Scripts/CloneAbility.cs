using UnityEngine;
using System.Collections;

public class CloneAbility : MonoBehaviour {

	public float spawnDuration = 5;
	public float cloneDuration = 60;
	public int maxSpawnSteps = 5;

	public GameObject cloneSpawnerPrefab;
	public GameObject clonePrefab;

	private float elapsedSpawnTime;
	private float elapsedCloneTime;
	private int elapsedSteps;
	private int initialSteps;

	private bool isActive = false;

	private GameObject cloneSpawner;
	private GameObject clone;

	private GridMovement playerMovement;

	void Start() {
		playerMovement = GetComponent<GridMovement> ();
	}

	void Update() {
		if (!isActive && Input.GetKeyDown (KeyCode.C) && !playerMovement.IsMoving ()) {
			ActivateClone ();
		}

		if (isActive && cloneSpawner) {

			elapsedSteps = cloneSpawner.GetComponent<GridMovement> ().Steps () - initialSteps;
			elapsedSpawnTime += Time.deltaTime;

			if (elapsedSteps >= maxSpawnSteps || elapsedSpawnTime > spawnDuration) {
				SpawnClone ();
			}
		}

		if (isActive && clone) {
			elapsedCloneTime += Time.deltaTime;

			if (elapsedCloneTime > cloneDuration) {
				DestroyClone ();
			}
		}

		if (isActive && !cloneSpawner && !clone) {
			isActive = false;
		}
	}

	public bool IsActive () {
		return isActive;
	}

	public GameObject GetClone () {
		return clone;
	}

	public void ActivateClone () {
		isActive = true;
		SpawnCloneSpawner ();
	}

	void SpawnCloneSpawner() {
		// disable player movement
		playerMovement.enabled = false;

		Vector3 spawnPoint = transform.position;

		// create clone spawner
		cloneSpawner = Instantiate (cloneSpawnerPrefab, spawnPoint, Quaternion.identity) as GameObject;
		//cloneSpawner.transform.parent = transform;

		initialSteps = cloneSpawner.GetComponent<GridMovement> ().Steps ();
		elapsedSpawnTime = 0;
	}

	public void SpawnClone() {
		Vector3 spawnPoint = RoundToNearestHalf (cloneSpawner.transform.position);

		DestroyCloneSpawner ();
		clone = Instantiate (clonePrefab, spawnPoint, Quaternion.identity) as GameObject;
		//clone.transform.parent = transform;

		GetComponent<GridMovement> ().enabled = true;
		clone.GetComponent <GridMovement> ().enabled = true;
		elapsedCloneTime = 0;
	}

	Vector3 RoundToNearestHalf (Vector3 position) {
		position *= 2;

		Vector3 rounded = new Vector3 (Mathf.Round (position.x),
			                           Mathf.Round (position.y),
			                           Mathf.Round (position.z));

		return rounded / 2;
	}

	IEnumerator DestroyCloneTimed() {
		yield return new WaitForSeconds(cloneDuration);

		DestroyClone ();
	}

	void DestroyCloneSpawner () {
		if (cloneSpawner) {
			Destroy (cloneSpawner);
			cloneSpawner = null;
		}
	}

	public void DestroyClone() {
		if (clone) {
			Destroy (clone);
			clone = null;

			isActive = false;
		}
	}
}
