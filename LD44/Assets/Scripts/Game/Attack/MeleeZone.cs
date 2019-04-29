using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeZone : MonoBehaviour {
	public bool use;
	public float dmg;
	public float cooldown;
	float currAttackTime;

	internal string enemyTag;

	List<Collider2D> triggerList;
	internal Attacker currAttacker;
	Attacker currEnemy;

	public bool IsInBattle => currEnemy != null;

	void Start() {
		triggerList = new List<Collider2D>();
		currEnemy = null;
	}

	void Update() {
		if(use && currEnemy != null){
			currAttackTime += Time.deltaTime;
			if (currAttackTime >= cooldown) {
				currAttackTime -= cooldown;
				if(currAttacker == null || currEnemy == null){

				}
				currAttacker.Attack(currEnemy, dmg);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (!collision.isTrigger && collision.tag == enemyTag && !triggerList.Contains(collision)) {
			if (triggerList.Count == 0) {
				currAttackTime = 0;
				currEnemy = collision.GetComponent<Attacker>();
			}
			triggerList.Add(collision);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (!collision.isTrigger && collision.tag == enemyTag && triggerList.Contains(collision)) {
			triggerList.Remove(collision);
			if (collision.GetComponent<Attacker>() == currEnemy)
				currEnemy = triggerList.Count == 0 ? null : triggerList[0].GetComponent<Attacker>();
		}
	}
}
