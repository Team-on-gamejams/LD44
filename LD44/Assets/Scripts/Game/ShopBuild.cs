using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuild : MonoBehaviour {
	public GameObject buildPrefab;
	public GameObject spawnedGameObject;
	public BuildForbidder buildForbidder;
	internal BuildingBase buildingBase;
	Image shopImage;
	TextMeshProUGUI text;

	bool isBuilding;
	bool isBuilded;
	float currBuildTime;

	public float startingAlpha;
	bool isShowed;

	void Start() {
		shopImage = transform.Find("ShopImage").GetComponent<Image>();
		text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
		buildingBase = buildPrefab.GetComponent<BuildingBase>();
		text.text += $" {buildingBase.price.meat}";
		isBuilded = false;
		isBuilding = false;
		startingAlpha = shopImage.color.a;

		shopImage = transform.Find("ShopImage").GetComponent<Image>();
		text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
		Hide();
	}

	void Update() {
		if(isBuilding){
			currBuildTime += Time.deltaTime;
			shopImage.fillAmount = currBuildTime / buildingBase.BuildTime;
			if (currBuildTime >= buildingBase.BuildTime) {
				shopImage.fillAmount = 1;
				isBuilding = false;
				isBuilded = true;
			}
		}
	}

	public void OnClick(){
		if (!isShowed)
			return;

		if (GameManager.Instance.Player.cursorMode == CursorMode.Normal) {
			if (isBuilded) {
				EnableBuildMode();
			}
			else if (!isBuilding) {
				if (GameManager.Instance.Player.CanTakeResource(buildingBase.price)) {
					GameManager.Instance.Player.TakeResource(buildingBase.price);
					SetAlpha(shopImage, 1f);
					isBuilding = true;
					currBuildTime = 0;
					shopImage.fillAmount = 0;
				}
			}
		}
	}

	public void EnableBuildMode(){
		SetAlpha(shopImage, startingAlpha);
		GameManager.Instance.Player.cursorMode = CursorMode.Build;
		GameManager.Instance.Player.currentBuildPref = this;

		spawnedGameObject = Instantiate(buildPrefab);
		spawnedGameObject.AddComponent<StayOnCursorPos>();
		buildForbidder = spawnedGameObject.AddComponent<BuildForbidder>();
	}

	public void DisableBuildMode() {
		if(isBuilded)
			SetAlpha(shopImage, 1.0f);
		GameManager.Instance.Player.cursorMode = CursorMode.Normal;
		GameManager.Instance.Player.currentBuildPref = null;
		spawnedGameObject = null;
	}

	public void Build() {
		isBuilded = false;
		DisableBuildMode();
	}


	void Awake() {
		EventManager.ChangeBuildingsListEvent += BloodLevelChangedEvent;
	}

	void OnDestroy() {
		EventManager.ChangeBuildingsListEvent -= BloodLevelChangedEvent;
	}

	void BloodLevelChangedEvent(EventData ed) {
		if (ed?.Data["Action"] as string == "Add") {
			BuildingType.TryParse(ed.Data["Value"] as string, out BuildingType type);
			if (type == buildingBase.awaliableAfter)
				Show();
		}
		else if (ed?.Data["Action"] as string == "Remove") {
			BuildingType.TryParse(ed.Data["Value"] as string, out BuildingType type);
			if (type == buildingBase.awaliableAfter)
				Hide();
		}
	}

	void Show() {
		SetAlpha(shopImage, startingAlpha);
		SetAlpha(text, 1.0f);
		isShowed = true;
	}

	void Hide() {
		SetAlpha(shopImage, 0.0f);
		SetAlpha(text, 0.0f);
		isShowed = false;
	}

	void SetAlpha(MaskableGraphic image, float a) {
		Color color = image.color;
		color.a = a;
		image.color = color;
	}

	void SetAlpha(Image image, float a){
		Color color = image.color;
		color.a = a;
		image.color = color;
	}
}
