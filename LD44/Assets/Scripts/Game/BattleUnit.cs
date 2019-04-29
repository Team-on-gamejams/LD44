﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleUnit : MonoBehaviour {
	internal bool isMoving;

	public abstract void Die();

	public abstract void MoveTo(Vector2 pos);
}
