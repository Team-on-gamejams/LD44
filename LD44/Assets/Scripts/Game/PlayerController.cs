using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CursorMode {
	Normal,
	Build,
}

public class PlayerController : MonoBehaviour {

	[SerializeField] List<BuildingType> buildingTypes;
	[SerializeField] List<BuildingBase> buildings;
	[SerializeField] List<BuildingBase> baraks;
	public List<BuildingBase> Baraks => baraks;

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

	public float bloodProd;
	public float bloodTake;

	void Start() {
		buildingTypes = new List<BuildingType>();
		buildings = new List<BuildingBase>();
		units = new List<UnitBase>();
		selectedUnits = new List<UnitBase>();
		baraks = new List<BuildingBase>();
		aINavMeshGenerator = GetComponent<AINavMeshGenerator>();
		unitSelection = GetComponent<UnitSelectionComponent>();
		currentBuildPref = null;
		cursorMode = CursorMode.Normal;
		bloodProd = 0;
		bloodTake = 0;

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
				if (!currentBuildPref.buildForbidder.CanBuild)
					return;
				AddBuilding(currentBuildPref.spawnedGameObject.GetComponent<BuildingBase>());
				Destroy(currentBuildPref.spawnedGameObject.GetComponent<StayOnCursorPos>());
				Destroy(currentBuildPref.buildForbidder);
				BuildInBuildingMode();
			}
			else if (Input.GetMouseButtonDown(1)) {
				Destroy(currentBuildPref.spawnedGameObject);
				DisableBuildingMode();
			}
		}
	}

	public void BuildInBuildingMode() {
		if (currentBuildPref != null) {
			currentBuildPref.Build();
		}
	}

	public void DisableBuildingMode(){
		if(currentBuildPref != null){
			currentBuildPref.DisableBuildMode();
		}
	}

	public void AddUnit(UnitBase unit){
		units.Add(unit);
		bloodTake += unit.bloodConsumper.bloodConsumpertion;
		GameManager.Instance.EventManager.CallOnBloodLevelChangedEvent();
	}

	public void AddSelectedUnit(UnitBase unit) {
		unit.Select();
		selectedUnits.Add(unit);
	}

	public void RemoveUnit(UnitBase unit) {
		units.Remove(unit);
		if (selectedUnits.Contains(unit))
			selectedUnits.Remove(unit);
		bloodTake -= unit.bloodConsumper.bloodConsumpertion;
		GameManager.Instance.EventManager.CallOnBloodLevelChangedEvent();
	}

	public void CleatSelectedUnits(){
		foreach (var unit in selectedUnits) 
			unit.UnSelect();
		selectedUnits.Clear();
	}

	public void AddBuilding(BuildingBase building){
		buildings.Add(building);
		maxRes += building.capacity;
		bloodProd += building.production.blood;
		bloodTake += building.bloodConsumper.bloodConsumpertion;
		GameManager.Instance.EventManager.CallOnBloodLevelChangedEvent();
		if (building.buildingType == BuildingType.Baraks)
			baraks.Add(building);

		if (!buildingTypes.Contains(building.buildingType)){
			buildingTypes.Add(building.buildingType);
			EventData ed = new EventData("AddBuilding");
			ed.Data["Action"] = "Add";
			ed.Data["Value"] = building.buildingType.ToString();
			GameManager.Instance.EventManager.CallOnChangeBuildingsListEvent(ed);
		}
	}

	public void RemoveBuilding(BuildingBase building) {
		buildings.Remove(building);
		if (baraks.Contains(building))
			baraks.Remove(building);
		maxRes -= building.capacity;
		bloodProd -= building.production.blood;
		bloodTake -= building.bloodConsumper.bloodConsumpertion;
		GameManager.Instance.EventManager.CallOnBloodLevelChangedEvent();

		bool findAnother = false;
		foreach (var i in buildings) {
			if(i.buildingType == building.buildingType){
				findAnother = true;
				break;
			}
		}
		if (!findAnother) {
			buildingTypes.Remove(building.buildingType);
			EventData ed = new EventData("AddBuilding");
			ed.Data["Action"] = "Remove";
			ed.Data["Value"] = building.buildingType.ToString();
			GameManager.Instance.EventManager.CallOnChangeBuildingsListEvent(ed);
		}
	}

	public void AddResource(GameRes res) {
		currRes += res;
		if (currRes.meat > maxRes.meat)
			currRes.meat = maxRes.meat;
	}

	public bool CanTakeResource(GameRes res) {
		return res <= currRes;
	}

	public void TakeResource(GameRes res){
		currRes -= res;
		if (currRes.meat < 0)
			currRes.meat = 0;
	}
}
