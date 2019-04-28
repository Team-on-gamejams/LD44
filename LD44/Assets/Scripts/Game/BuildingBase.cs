using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour {
	public BloodConsumper bloodConsumper;
	public Health health;
	public Price price;
	public Production production;
	public Capacity capacity;

	public float BuildTime = 1.0f;

	public GameObject buildingSpriteAvaliable;
	public GameObject buildingSpriteUnAvaliable;

	void Start() {
		buildingSpriteAvaliable = transform.Find("BuildingSpriteAvaliable").gameObject;
		buildingSpriteUnAvaliable = transform.Find("BuildingSpriteUnAvaliable").gameObject;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		if(bloodConsumper.TimeForConsumpertion != 0)
			bloodConsumper.Tick(Time.deltaTime);
		if(production.TimeForProd != 0)
			production.Tick(Time.deltaTime);
	}
}

