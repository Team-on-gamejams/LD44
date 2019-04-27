using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
	List<BuildingBase> buildings;

	const float gameTickTime = 1.0f;
	float currTickTime = 0f;

	void Start() {
		buildings = new List<BuildingBase>();

		GameManager.Instance.Player = this;
	}

	void Update() {
		currTickTime += Time.deltaTime;

		while(currTickTime >= gameTickTime){
			currTickTime -= gameTickTime;

			foreach (var building in buildings)
				building.GameTick();
		}
	}
}
