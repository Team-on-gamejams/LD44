using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BloodConsumper {
	public float bloodConsumpertion;
	GameRes res;

	public float TimeForConsumpertion = 1.0f;
	float currTime = 0f;

	public BloodConsumper() {
		res = new GameRes();
	}

	public void Tick(float deltaTime) {
		currTime += deltaTime;
		while (currTime >= TimeForConsumpertion) {
			currTime -= TimeForConsumpertion;
			GameManager.Instance.Player.TakeResource(res);
		}
	}
}
