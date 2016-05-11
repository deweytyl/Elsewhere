using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public string nextSceneName;

	private const string ASSETS_DIR_PATH = "Assets/";

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			StartCoroutine (ChangeLevel ());
		}
	}

	IEnumerator ChangeLevel () {
		float fadeTime = GetComponent<LevelFader> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

		SceneManager.LoadScene (nextSceneName);
	}
}
