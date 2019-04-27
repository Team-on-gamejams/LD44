using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CursorMode {
	Normal,
	Build,
}

public class PlayerController : MonoBehaviour {

	[SerializeField] List<BuildingBase> buildings;

	[SerializeField] List<UnitBase> units;
	public List<UnitBase> Units => units;
	[SerializeField] List<UnitBase> selectedUnits;

	[SerializeField] GameRes currRes;
	public GameRes CurrRes => currRes;
	[SerializeField] GameRes maxRes;
	public GameRes MaxRes => maxRes;

	public CursorMode cursorMode;
	public ShopBuild currentBuildPref;

	public UnitSelectionComponent unitSelection;
	public AINavMeshGenerator aINavMeshGenerator;

	const float gameTickTime = 1.0f;
	float currTickTime = 0f;

	void Start() {
		buildings = new List<BuildingBase>();
		units = new List<UnitBase>();
		selectedUnits = new List<UnitBase>();
		aINavMeshGenerator = GetComponent<AINavMeshGenerator>();
		unitSelection = GetComponent<UnitSelectionComponent>();
		currentBuildPref = null;
		cursorMode = CursorMode.Normal;

		GameManager.Instance.Player = this;
	}

	void Update() {
		if(cursorMode == CursorMode.Normal){
			if (Input.GetMouseButtonDown(1)) {
				var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				foreach (var unit in units)
					unit.RecalcMoveIfStop();
				foreach (var unit in selectedUnits)
					unit.MoveTo(clickPos);
			}
		}
		else if (cursorMode == CursorMode.Build) {
			if (Input.GetMouseButtonDown(0)) {
				AddBuilding(currentBuildPref.spawnedGameObject.GetComponent<BuildingBase>());
				Destroy(currentBuildPref.spawnedGameObject.GetComponent<StayOnCursorPos>());
				DisableBuildingMode();
			}
			else if (Input.GetMouseButtonDown(1)) {
				Destroy(currentBuildPref.spawnedGameObject);
				DisableBuildingMode();
			}
		}
	}

	public void DisableBuildingMode(){
		if(currentBuildPref != null){
			currentBuildPref.DisableBuildMode();
		}
	}

	public void AddUnit(UnitBase unit){
		units.Add(unit);
	}

	public void AddSelectedUnit(UnitBase unit) {
		unit.Select();
		selectedUnits.Add(unit);
	}

	public void CleatSelectedUnits(){
		foreach (var unit in selectedUnits) 
			unit.UnSelect();
		selectedUnits.Clear();
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

	public void TakeBlood(float res) {
		currRes.blood -= res;
		if (currRes.blood < 0)
			currRes.blood = 0;
	}
}
