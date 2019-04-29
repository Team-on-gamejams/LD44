using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType {
	None,
	MainHearth,
	AdditionalHearth,
	Baraks,
}

public class BuildingBase : MonoBehaviour {
	public BloodConsumper bloodConsumper;
	public Health health;
	public Price price;
	public Production production;
	public Capacity capacity;

	public BuildingType awaliableAfter = BuildingType.None;
	public BuildingType buildingType = BuildingType.None;
	public float BuildTime = 1.0f;

	public GameObject buildingSpriteAvaliable;
	public GameObject buildingSpriteUnAvaliable;

	void Start() {
		buildingSpriteAvaliable = transform.Find("BuildingSpriteAvaliable").gameObject;
		buildingSpriteUnAvaliable = transform.Find("BuildingSpriteUnAvaliable").gameObject;

		health.Init();
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		if(production.TimeForProd != 0)
			production.Tick(Time.deltaTime);
	}
}

