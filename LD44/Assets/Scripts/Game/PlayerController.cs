using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
	List<BuildingBase> buildings;

	GameRes currRes;
	GameRes maxRes;

	const float gameTickTime = 1.0f;
	float currTickTime = 0f;

	void Start() {
		buildings = new List<BuildingBase>();

		GameManager.Instance.Player = this;
	}

	public void AddResource(GameRes res) {
		currRes += res;
		if (currRes.blood > maxRes.blood)
			currRes.blood = maxRes.blood;
		if (currRes.meat > maxRes.meat)
			currRes.meat = maxRes.meat;
	}

	public bool CanTakeResource(GameRes res) {
		return res <= currRes;
	}

	public void TakeResource(GameRes res){
		currRes -= res;
	}
}
