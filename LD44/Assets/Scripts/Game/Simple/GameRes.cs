using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameRes {
	public float blood;
	public float meat;

	public GameRes() {
		meat = blood = 0;
	}

	public GameRes(float _blood, float _meat) {
		meat = _meat;
		blood = _blood;
	}

	public static bool operator ==(GameRes r1, GameRes r2) => r1.meat == r2.meat;
	public static bool operator !=(GameRes r1, GameRes r2) => r1.meat != r2.meat;
	public static bool operator <(GameRes r1, GameRes r2) => r1.meat < r2.meat;
	public static bool operator >(GameRes r1, GameRes r2) =>r1.meat > r2.meat;
	public static bool operator <=(GameRes r1, GameRes r2) =>  r1.meat <= r2.meat;
	public static bool operator >=(GameRes r1, GameRes r2) =>  r1.meat >= r2.meat;

	public static GameRes operator +(GameRes r1, GameRes r2) {
		GameRes temp = new GameRes();
		temp.meat = r1.meat + r2.meat;
		return temp;
	}

	public static GameRes operator-(GameRes r1, GameRes r2) {
		GameRes temp = new GameRes();
		temp.meat = r1.meat - r2.meat;
		return temp;
	}
}
