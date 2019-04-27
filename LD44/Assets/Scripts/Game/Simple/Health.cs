using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health {
	public float maxHealth;
	float points;

	public Health() {
		points = maxHealth;
	}

	public void Set(float val) {
		points = val;
		if(points > maxHealth)
			points = maxHealth;
	}
	public float Get() => points;

	public bool IsDead() => points <= 0;
	public bool IMaxHP() => points == maxHealth;

	public void TakeDmg(float dmg) => points -= dmg;
	public void Heal(float val) {
		points += val;
		if (points > maxHealth)
			points = maxHealth;
	}
}
