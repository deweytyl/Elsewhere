using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class BlowBack : MonoBehaviour {

	public float time;

	private GameObject affected;
	private bool inside;
	private bool excuted;
	private bool cloned;
	private GameObject clone;
	private Object myClone;

	void Start () {
		excuted = false;
		cloned = false;
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer ==  LayerMask.NameToLayer("Moveble")) {
			inside = true;
			affected = other.gameObject;
		}

	}

	void Update () {
		if (inside) {
			if (Input.GetKey (KeyCode.J)) {
				transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
				affected.gameObject.transform.position = new Vector3 
					(affected.gameObject.transform.position.x + 3, affected.gameObject.transform.position.y, affected.gameObject.transform.position.z);
			}
		}
		if (Input.GetKey (KeyCode.K) && !excuted && !cloned) {
			excuted = true;
			cloned = true;
			Invoke ("Active", 0.2f);
		} 

	}

	void Active () {
			clone = new GameObject (); 
			clone = gameObject;
			myClone = Instantiate (clone, new Vector2 (transform.position.x, transform.position.y+3), Quaternion.identity);
			Destroy (myClone, 2.0f);

	}

	void OnTriggerExit2D (Collider2D other) {
		inside = false;
	}

}
