using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagController : MonoBehaviour {
	Image flag;

	void Start() {
		flag = GetComponent<Image>();
	}

	void Update() {
		if (GameManager.Instance.Player.cursorMode == CursorMode.Normal && Input.GetMouseButtonDown(1) && GameManager.Instance.Player.SelectedUnits.Count != 0) {
			flag.rectTransform.position = Input.mousePosition;
			Animate();
		}
	}

	void Animate(){
		Color color = flag.color;
		color.a = 1;
		flag.color = color;
		flag.transform.localScale = new Vector3(1, 1, 1) / 2;

		LeanTween.value(gameObject, 1, 0, 1f)
		.setOnUpdate((float a) => {
			color = flag.color;
			color.a = a;
			flag.color = color;
			flag.transform.localScale = new Vector3(a / 2, a / 2, a / 2);
		});
	}
}
