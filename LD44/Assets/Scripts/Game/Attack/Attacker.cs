using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {
	public string enemyTag;

	public GameObject melee;
	MeleeZone meleeZone;
	public GameObject range;
	public GameObject autoAttackRange;
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
	}

	public bool Attack(Attacker enemy, float dmg){
		enemy.health.TakeDmg(dmg);
		if(enemy.health.IsDead()){
			if (enemyTag == "Unit") {
				GameManager.Instance.Player.RemoveUnit(enemy.GetComponent<UnitBase>());
				Destroy(enemy.gameObject);
			}
			else if (enemyTag == "Enemy") {
				Destroy(enemy.gameObject);
			}
			return true;
		}
		return false;
	}
}
