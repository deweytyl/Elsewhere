using UnityEngine;
using System.Collections;

public class PersistentSingleton : MonoBehaviour {

	public static PersistentSingleton instance;

	protected void Awake () {
		if (instance == null) {
			DontDestroyOnLoad(gameObject);
			instance = this;

		} else if (instance != this) {
			Destroy(gameObject);
		}
	}
}
