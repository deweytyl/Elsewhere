using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour {

	[System.Serializable]
	public struct TilePrefabMapping {
		public string name;
		public GameObject prefab;
	}

	public TextAsset roomMapCSV;
	public GameObject roomContainerPrefab;
	public List<TilePrefabMapping> tileMappingsList;

	private string[, ] roomMap;
	private GameObject roomContainer;
	private Dictionary<string, GameObject> tileMappings;

	// ----------------------------------------------------
	// Public Editor-facing methods

	public void GenerateRoom () {
		DestroyRoom ();

		LoadRoomMap ();
		LoadTileMappings ();

		CreateRoom ();
	}

	public void DestroyRoom() {
		GameObject[] roomContainers = GameObject.FindGameObjectsWithTag ("RoomContainer");

		foreach (GameObject roomContainer in roomContainers) {
			DestroyImmediate (roomContainer);
		}
	}

	// ----------------------------------------------------
	// Helper Methods

	void LoadRoomMap() {
		roomMap = CSVReader.SplitCsvGrid (roomMapCSV.text);
		CSVReader.DebugOutputGrid (roomMap);
	}

	void LoadTileMappings() {
		tileMappings = new Dictionary<string, GameObject> ();

		foreach (TilePrefabMapping mapping in tileMappingsList) {
			tileMappings.Add (mapping.name, mapping.prefab);
		}
	}

	void CreateRoom() {
		roomContainer = Instantiate (
			roomContainerPrefab, 
			Vector3.zero, 
			Quaternion.identity
		) as GameObject;

		roomContainer.transform.parent = transform;
		roomContainer.transform.localPosition = Vector3.zero;

		int height = roomMap.GetUpperBound(1); // 2nd dimension = height
		int width = roomMap.GetUpperBound(0); // 1st dimension = width

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				string tileName = roomMap [x, y];

				if (x == 3)
					Debug.Log ("X = 3");

				InstantiateTile (tileName, x, y);
			}
		}
	}

	void InstantiateTile (string tileName, int x, int y) {
		if (tileMappings.ContainsKey (tileName)) {
			Vector3 tilePosition = new Vector3 (x, -y);

			GameObject tilePrefab = tileMappings [tileName];

			GameObject tile = Instantiate (tilePrefab, tilePosition, Quaternion.identity)
				as GameObject;
			
			tile.transform.parent = roomContainer.transform;
			tile.transform.localPosition = tilePosition;
		}
	}
}
