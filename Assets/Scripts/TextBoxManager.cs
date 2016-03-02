using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager: MonoBehaviour {

	public GameObject textBox;
	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int curLine;
	public int lastLine;

	public PlayerControl player;

	public bool isActive;
	public bool stopPlayerMovement;

	private bool isTyping = false;
	private bool cancelTyping = false;
	public float typeSpeed;

	void Start () {

		player = FindObjectOfType<PlayerControl>();

		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}
		if (lastLine == 0) {
			lastLine = textLines.Length - 1;
		}

		if (isActive) {
			EnableTextBox ();
		} else {
			DisableTextBox ();
		}

	}

	void Update() {
		if (!isActive) {
			return;
		}

		//theText.text = textLines [curLine];

		if (Input.GetKeyDown (KeyCode.Return)) {
			if (!isTyping) {
				curLine++;
				if (curLine > lastLine) {
					DisableTextBox ();
				} else {
					StartCoroutine (TextScroll(textLines[curLine]));
				}
			} else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			}
		}
	}

	private IEnumerator TextScroll (string lineOfTexts) {
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfTexts.Length - 1)) {
			theText.text += lineOfTexts [letter++];
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfTexts;
		isTyping = false;
		cancelTyping = true;
	}

	public void EnableTextBox() {
		textBox.SetActive (true);
		isActive = true;
		if (stopPlayerMovement) {
			player.moveble = false;
		}
		StartCoroutine (TextScroll(textLines[curLine]));
	}

	public void DisableTextBox() {
		textBox.SetActive (false);
		isActive = false;
		player.moveble = true;
	}

	public void ReloadScript(TextAsset newText) {
		if (newText != null) {
			textLines = new string[1];
			textLines = (newText.text.Split ('\n'));
		}
	}

}
