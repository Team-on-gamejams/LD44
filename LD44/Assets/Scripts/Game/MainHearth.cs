﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHearth : BuildingBase {
	SpriteRenderer sp;

	void Start() {
		sp = GetComponent<SpriteRenderer>();
		sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);

		Color color = sp.color;
		color.a = 0;
		sp.color = color;

		LeanTween.value(gameObject, sp.color.a, 1, 1.0f)
		.setOnUpdate((float alpha) => {
			color = sp.color;
			color.a = alpha;
			sp.color = color;
		})
		.setOnComplete(()=> {
			GameManager.Instance.Player.AddBuilding(this);
		});
	}
}
