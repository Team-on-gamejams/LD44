using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHearth : BuildingBase {
	void Start() {
		LeanTween.delayedCall(1f, () => {
			GameManager.Instance.Player.AddBuilding(this);
		});
	}
}
