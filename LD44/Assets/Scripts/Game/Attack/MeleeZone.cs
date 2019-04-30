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
	internal AutoAttackZone autoAttackZone;

	public bool IsInBattle => currEnemy != null && !autoAttackZone.unit.isMoving;

	void Start() {
		triggerList = new List<Collider2D>();
		currEnemy = null;
	}

	void Update() {
		if(use && currEnemy != null && !autoAttackZone.unit.isMoving){
			currAttackTime += Time.deltaTime;
			if (currAttackTime >= cooldown) {
				//autoAttackZone.unit.anim.SetTrigger("Attack");
				currAttackTime -= cooldown;
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
			if (collision.GetComponent<Attacker>() == currEnemy){
				if(triggerList.Count == 0){
					currEnemy = null;
				}
				else {
					currEnemy = triggerList[0].GetComponent<Attacker>();
				}
			}
		}
	}
}
