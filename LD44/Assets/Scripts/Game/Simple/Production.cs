using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Production : GameRes{
	public float TimeForProd = 1.0f;
	float currTime = 0f;

	public void Tick(float deltaTime) {
		currTime += deltaTime;
		while(currTime >= TimeForProd) {
			currTime -= TimeForProd;
			GameManager.Instance.Player.AddResource(this);
		}
	}
}
