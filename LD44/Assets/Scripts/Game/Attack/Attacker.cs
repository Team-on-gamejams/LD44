using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {
	public string enemyTag;

	public GameObject melee;
	MeleeZone meleeZone;
	public GameObject range;
	public GameObject autoAttackRange;
	AutoAttackZone autoAttackZone;
	bool canMove;

	Health health;

	void Start() {
		if(enemyTag  == "Unit"){
			EnemyBase @base = GetComponent<EnemyBase>();
			health =  @base.health;
			canMove = @base.speed != 0;
		}
		else if (enemyTag == "Enemy") {
			UnitBase  @base = GetComponent<UnitBase>();
			health =  @base.health;
			canMove = @base.speed != 0;
		}

		meleeZone = melee.GetComponent<MeleeZone>();
		if(meleeZone.use){
			meleeZone.enemyTag = enemyTag;
			meleeZone.currAttacker = this;
		}

		autoAttackZone = autoAttackRange.GetComponent<AutoAttackZone>();
		autoAttackZone.enemyTag = enemyTag;
		autoAttackZone.meleeZone = meleeZone;
		if (enemyTag == "Unit") 
			autoAttackZone.unit = GetComponent<EnemyBase>();
		else if (enemyTag == "Enemy") 
			autoAttackZone.unit = GetComponent<UnitBase>();
	}

	public bool Attack(Attacker enemy, float dmg){
		enemy.health.TakeDmg(dmg);
		if(enemy.health.IsDead()){
			if (enemyTag == "Unit") {
				enemy.GetComponent<UnitBase>().Die();
			}
			else if (enemyTag == "Enemy") {
				enemy.GetComponent<EnemyBase>().Die();
			}
			return true;
		}
		return false;
	}
}
