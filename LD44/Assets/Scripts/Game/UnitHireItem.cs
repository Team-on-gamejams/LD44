using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHireItem : MonoBehaviour {
	public GameObject buildPrefab;
	internal UnitBase unitBase;
	Image shopImage;
	TextMeshProUGUI text;
	TextMeshProUGUI hireCountText;

	float currBuildTime;

	public float startingAlpha;
	bool isShowed;
	int hireCount;

	void Start() {
		shopImage = transform.Find("ShopImage").GetComponent<Image>();
		text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
		hireCountText = transform.Find("HireCount").GetComponent<TextMeshProUGUI>();
		unitBase = buildPrefab.GetComponent<UnitBase>();
		text.text += $" {unitBase.price.meat}";
		startingAlpha = shopImage.color.a;
		hireCount = 0;
		hireCountText.text = hireCount.ToString();

		shopImage = transform.Find("ShopImage").GetComponent<Image>();
		text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
		Hide();
	}

	void Update() {
		if (hireCount != 0) {
			currBuildTime += Time.deltaTime;
			shopImage.fillAmount = currBuildTime / unitBase.buildTime;
			if (currBuildTime >= unitBase.buildTime) {
				shopImage.fillAmount = 1;

				foreach (var barak in GameManager.Instance.Player.Baraks) {
					if(hireCount != 0){
						--hireCount;
						Instantiate(buildPrefab, barak.transform);
					}
				}
				hireCountText.text = hireCount.ToString();

				if (hireCount != 0){
					SetAlpha(shopImage, 1f);
					currBuildTime = 0;
					shopImage.fillAmount = 0;
				}
				else{
					SetAlpha(shopImage, startingAlpha);
				}
			}
		}
	}

	public void OnClick() {
		if (!isShowed)
			return;

		if (GameManager.Instance.Player.cursorMode == CursorMode.Normal) {
			if (GameManager.Instance.Player.CanTakeResource(unitBase.price)) {
				GameManager.Instance.Player.TakeResource(unitBase.price);
				++hireCount;
				hireCountText.text = hireCount.ToString();
			}
		}
	}

	void Awake() {
		EventManager.ChangeBuildingsListEvent += ChangeBuildingsListEvent;
	}

	void OnDestroy() {
		EventManager.ChangeBuildingsListEvent -= ChangeBuildingsListEvent;
	}

	void ChangeBuildingsListEvent(EventData ed) {
		if (ed?.Data["Action"] as string == "Add") {
			BuildingType.TryParse(ed.Data["Value"] as string, out BuildingType type);
			if (type == unitBase.awaliableAfter)
				Show();
		}
		else if (ed?.Data["Action"] as string == "Remove") {
			BuildingType.TryParse(ed.Data["Value"] as string, out BuildingType type);
			if (type == unitBase.awaliableAfter)
				Hide();
		}
	}

	void Show() {
		SetAlpha(shopImage, startingAlpha);
		SetAlpha(text, 1.0f);
		SetAlpha(hireCountText, 1.0f);
		isShowed = true;
	}

	void Hide() {
		SetAlpha(shopImage, 0.0f);
		SetAlpha(text, 0.0f);
		SetAlpha(hireCountText, 0.0f);
		isShowed = false;
	}

	void SetAlpha(MaskableGraphic image, float a) {
		Color color = image.color;
		color.a = a;
		image.color = color;
	}

	void SetAlpha(Image image, float a) {
		Color color = image.color;
		color.a = a;
		image.color = color;
	}
}
