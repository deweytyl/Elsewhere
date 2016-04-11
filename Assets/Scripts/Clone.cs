using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {

	public float spawnDuration;
	public float cloneDuration;
	public int maxSpawnSteps;

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
		if (!isActive && !playerMovement.IsMoving () && Input.GetKeyDown (KeyCode.C)) {
			ActivateClone ();
		}

		if (isActive && cloneSpawner) {

			elapsedSteps = cloneSpawner.GetComponent<GridMovement> ().Steps () - initialSteps;
			elapsedSpawnTime += Time.deltaTime;

			if (elapsedSteps >= 5 || elapsedSpawnTime > 5) {
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

	void ActivateClone () {
		isActive = true;
		SpawnCloneSpawner ();
	}

	void SpawnCloneSpawner() {
		// disable player movement
		playerMovement.enabled = false;

		Vector3 spawnPoint = transform.position;

		// create clone spawner
		cloneSpawner = Instantiate (cloneSpawnerPrefab, spawnPoint, Quaternion.identity) as GameObject;

		initialSteps = cloneSpawner.GetComponent<GridMovement> ().Steps ();
		elapsedSpawnTime = 0;
	}

	public void SpawnClone() {
		clone = Instantiate (clonePrefab, cloneSpawner.transform.position, Quaternion.identity) as GameObject;
		clone.gameObject.name = "Clone";

		Collider2D[] underneath = Physics2D.OverlapCircleAll (clone.transform.position, .2f);
		foreach (Collider2D collider in underneath) {
			if (collider.gameObject != clone && collider.gameObject != cloneSpawner && collider.GetComponent<Hole> ()) {
				DestroyClone ();
			}
		}

		DestroyCloneSpawner ();

		GetComponent<GridMovement> ().enabled = true;
		clone.GetComponent<GridMovement> ().enabled = true;
		elapsedCloneTime = 0;
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
