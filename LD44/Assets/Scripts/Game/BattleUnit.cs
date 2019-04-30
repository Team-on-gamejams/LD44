using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleUnit : MonoBehaviour {
	internal bool isMovingByPlayer;
	internal bool isMoving;

	void Start() {
		isMovingByPlayer = false;
	}

	public abstract void Die();

	public abstract void MoveTo(Vector2 pos);
}
