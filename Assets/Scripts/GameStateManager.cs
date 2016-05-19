using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : PersistentSingleton {

	public static GameStateManager manager; // singleton instance

	public string gameFinishedSceneName;

	bool completedIceRooms = false;
	bool completedTeleRooms = false;

	void Awake() {
		base.Awake ();
		manager = this;
	}

	public void CompleteIceRooms() {
		completedIceRooms = true;
		CheckForGameCompletion ();
	}

	public void CompleteTeleRooms() {
		completedTeleRooms = true;
		CheckForGameCompletion ();
	}

	void CheckForGameCompletion() {
		if (completedIceRooms && completedTeleRooms) {
			SceneManager.LoadScene (gameFinishedSceneName);
		}
	}
}
