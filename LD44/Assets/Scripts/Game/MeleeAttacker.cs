using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {
	Melee,
}

public class MeleeAttacker : MonoBehaviour {
	public AttackType attackType;
	
	public float maxXp;
	
	public float dmg;
	public float cooldown;
}
