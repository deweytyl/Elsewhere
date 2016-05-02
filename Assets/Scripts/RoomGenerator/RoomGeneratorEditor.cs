using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		RoomGenerator roomGenerator = target as RoomGenerator;

		if (GUILayout.Button ("Generate Room")) {
			roomGenerator.GenerateRoom ();
		}

		if (GUILayout.Button ("Delete Room")) {
			roomGenerator.DestroyRoom ();
		}

		DrawDefaultInspector ();
	}
}
