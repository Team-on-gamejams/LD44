using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnCursorPos : MonoBehaviour {
	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(newPos.x, newPos.y, -0.2f);
	}
}
