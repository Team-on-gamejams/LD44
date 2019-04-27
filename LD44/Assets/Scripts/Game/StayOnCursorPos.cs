using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnCursorPos : MonoBehaviour {
	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
