using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float displacement;
	public bool moveble;

	void Start () {
	}
	void FixedUpdate()
	{
		if (moveble) {
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				transform.Translate (0, Time.deltaTime * displacement, 0);
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				transform.Translate (0, -Time.deltaTime * 10, 0);
			} 
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				transform.Translate (Time.deltaTime * 10, 0, 0);
			} 
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				transform.Translate (-Time.deltaTime * 10, 0, 0);
			}
		} else {
			transform.Translate (0, 0, 0);
		}
			

	} 
//
//	void FixedUpdate () {
//
//			if (!moveble) {
//				return;
//			}
//			float moveHorizaontal = Input.GetAxisRaw ("Horizontal");
//			float moveVertical = Input.GetAxisRaw ("Vertical");
//			Vector2 movement = new Vector2 (moveHorizaontal, moveVertical);
//			rg.AddForce (movement * speed);
//	
//	}
}
