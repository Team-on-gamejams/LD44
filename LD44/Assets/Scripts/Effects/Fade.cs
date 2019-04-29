using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
	void Start() {
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
		Destroy(this);
	}
}
