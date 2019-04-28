using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuild : MonoBehaviour {
	public GameObject buildPrefab;
	public GameObject spawnedGameObject;
	public BuildForbidder buildForbidder;
	Image shopImage;

	void Start() {
		shopImage = transform.Find("ShopImage").GetComponent<Image>();
	}

	public void OnClick(){
		EnableBuildMode();
	}

	public void EnableBuildMode(){
		Color color = shopImage.color;
		color.a = 0.55f;
		shopImage.color = color;

		GameManager.Instance.Player.cursorMode = CursorMode.Build;
		GameManager.Instance.Player.currentBuildPref = this;

		spawnedGameObject = Instantiate(buildPrefab);
		spawnedGameObject.AddComponent<StayOnCursorPos>();
		buildForbidder = spawnedGameObject.AddComponent<BuildForbidder>();
	}

	public void DisableBuildMode() {
		Color color = shopImage.color;
		color.a = 1.0f;
		shopImage.color = color;
		GameManager.Instance.Player.cursorMode = CursorMode.Normal;
		GameManager.Instance.Player.currentBuildPref = null;
		spawnedGameObject = null;
	}
}
