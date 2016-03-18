using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Trigger : MonoBehaviour {
	
	public List<GameObject> listeners;

	public void EnableListenerTriggers () {
		foreach (var listener in listeners) {
			if (listener != null) {
				listener.SendMessage ("EnableTrigger", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void DisableListenerTriggers () {
		foreach (var listener in listeners) {
			if (listener != null) {
				listener.SendMessage ("DisableTrigger", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
