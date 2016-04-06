using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {

	public float spawnDuration;
	public float cloneDuration;
	public GameObject cloneSpawnerPrefab;
	public GameObject clonePrefab;

	private bool isActive = false;
	private GameObject cloneSpawner;
	private GameObject clone;

	void Update () {
		if (!isActive && Input.GetKeyDown (KeyCode.C)) {
			ActivateClone ();
		}
	}

	void ActivateClone () {
		isActive = true;

		// disable player movement
		GetComponent<GridMovement>().enabled = false;

		// create clone spawner
		cloneSpawner = Instantiate (cloneSpawnerPrefab, transform.position + Vector3.right, Quaternion.identity) as GameObject;
		StartCoroutine (SpawnClone (spawnDuration));
	}

	IEnumerator SpawnClone(float duration) {
		yield return new WaitForSeconds(duration);

		clone = Instantiate (clonePrefab, cloneSpawner.transform.position, Quaternion.identity) as GameObject;
		Destroy (cloneSpawner);
		cloneSpawner = null;

		GetComponent<GridMovement> ().enabled = true;
		clone.GetComponent<GridMovement> ().enabled = true;

		StartCoroutine (DestroyClone (cloneDuration));
	}

	IEnumerator DestroyClone (float duration) {
		yield return new WaitForSeconds(duration);

		Destroy (clone);
		clone = null;

		isActive = false;
	}
}
