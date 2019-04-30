using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackZone : MonoBehaviour {
	internal string enemyTag;
	internal BattleUnit unit;
	internal MeleeZone meleeZone;

	[SerializeField] List<Collider2D> triggerList;

	void Start() {
		triggerList = new List<Collider2D>();
	}

	void Update() {
		if(!meleeZone.IsInBattle) {
			if (triggerList.Count != 0) {
				if(
					!unit.isMovingByPlayer //&&
					//((unit.transform.position - triggerList[0].transform.position).magnitude > meleeZone.GetComponent<CircleCollider2D>().radius / 2)
				)
					unit.MoveTo(triggerList[0].transform.position);
			}
			else if (unit is EnemyBase){
				((EnemyBase)(unit)).TryMoveToHearth();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (!collision.isTrigger && collision.tag == enemyTag && !triggerList.Contains(collision)) {
			triggerList.Add(collision);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (!collision.isTrigger && collision.tag == enemyTag && triggerList.Contains(collision)) {
			triggerList.Remove(collision);
		}
	}
}
