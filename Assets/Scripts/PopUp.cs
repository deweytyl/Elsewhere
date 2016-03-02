using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

	//public Texture2D source;
	public GameObject light;

	private bool toggleGUI;

	public void Start() {
		light = GameObject.Find("Door/Light");
		light.SetActive (false);

	}

	public void OnTriggerEnter () {
		light.SetActive (true);
	}

}
