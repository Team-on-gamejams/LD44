using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	List<BuildingBase> buildings;

	[SerializeField]
	GameRes currRes;
	public GameRes CurrRes => currRes;
	[SerializeField]
	GameRes maxRes;
	public GameRes MaxRes => maxRes;

	const float gameTickTime = 1.0f;
	float currTickTime = 0f;

	void Start() {
		buildings = new List<BuildingBase>();

		GameManager.Instance.Player = this;
	}

	public void AddBuilding(BuildingBase building){
		buildings.Add(building);
		maxRes += building.capacity;
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
		if (currRes.blood < 0)
			currRes.blood = 0;
		if (currRes.meat < 0)
			currRes.meat = 0;
	}
}
