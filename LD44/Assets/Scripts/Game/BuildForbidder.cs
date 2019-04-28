using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildForbidder : MonoBehaviour {
	public bool CanBuild;
	Collider2D collider;
	BuildingBase building;

	void Start() {
		building = GetComponent<BuildingBase>();
		collider = GetComponent<Collider2D>();
		collider.isTrigger = true;

		Allow();
	}

	void OnDestroy() {
		collider.isTrigger = false;
		building.buildingSpriteUnAvaliable.SetActive(false);
		building.buildingSpriteAvaliable.SetActive(false);
	}

	void OnTriggerStay2D(Collider2D collision) {
		Disallow();
	}

	void OnTriggerExit2D(Collider2D other) {
		Allow();
	}

	void Allow(){
		building.buildingSpriteUnAvaliable.SetActive(false);
		building.buildingSpriteAvaliable.SetActive(true);
		CanBuild = true;
	}

	void Disallow(){
		building.buildingSpriteUnAvaliable.SetActive(true);
		building.buildingSpriteAvaliable.SetActive(false);
		CanBuild = false;
	}
}
