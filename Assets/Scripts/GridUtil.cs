using UnityEngine;
using System.Collections;

public class GridUtil {

	public static Vector3 RoundToNearestHalf (Vector3 position) {
		position *= 2;

		Vector3 rounded = new Vector3 (Mathf.Round (position.x),
			Mathf.Round (position.y),
			Mathf.Round (position.z));

		return rounded / 2;
	}
}
