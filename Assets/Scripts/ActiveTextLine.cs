using UnityEngine;
using System.Collections;

public class ActiveTextLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;
	public TextBoxManager textManager;

	public bool destroy;

	public bool requirePress;
	private bool waitForPress;

	void Start () {
		textManager = FindObjectOfType<TextBoxManager> ();
	}


	void Update () {
		if (waitForPress && Input.GetKeyDown (KeyCode.E)) {
			textManager.ReloadScript (theText);
			textManager.curLine = startLine;
			textManager.lastLine = endLine;
			textManager.EnableTextBox ();
			if (destroy) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (requirePress) {
			waitForPress = true;
			return;
		}
		if (other.name == "Char") {
			textManager.ReloadScript (theText);
			textManager.curLine = startLine;
			textManager.lastLine = endLine;
			textManager.EnableTextBox ();
			if (destroy) {
				Destroy (gameObject);
			}
		}

	}

	void OnTriggerExit2D (Collider2D other) {

		if (requirePress) {
			waitForPress = false;
		}
	}

}
