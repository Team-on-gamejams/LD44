using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour {
	public BloodConsumper bloodConsumper;
	public Health health;
	public Price price;
	public Production production;
	public Capacity capacity;

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		bloodConsumper.Tick(Time.deltaTime);
		production.Tick(Time.deltaTime);
	}
}

