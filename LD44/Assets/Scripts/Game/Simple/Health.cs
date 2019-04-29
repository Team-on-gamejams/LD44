using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health {
	public float maxHealth;
	[SerializeField] float currHealth;

	public void Init(){
		currHealth = maxHealth;
	}

	public void Set(float val) {
		currHealth = val;
		if(currHealth > maxHealth)
			currHealth = maxHealth;
	}
	public float Get() => currHealth;

	public bool IsDead() => currHealth <= 0;
	public bool IMaxHP() => currHealth == maxHealth;

	public void TakeDmg(float dmg) => currHealth -= dmg;
	public void Heal(float val) {
		currHealth += val;
		if (currHealth > maxHealth)
			currHealth = maxHealth;
	}
}
