using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleUnit : MonoBehaviour {
	public abstract void Die();

	public abstract void MoveTo(Vector2 pos);
}
