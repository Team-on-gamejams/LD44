using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
	CanvasGroup buildMenu;
	CanvasGroup hireMenu;
	Button buildButton;
	Button hireButton;

	void Start() {
		buildMenu = transform.Find("BuildPanel").gameObject.GetComponent<CanvasGroup>();
		hireMenu = transform.Find("HirePanel").gameObject.GetComponent<CanvasGroup>();
		buildButton = transform.Find("ButtonBuild").gameObject.GetComponent<Button>();
		hireButton = transform.Find("ButtonHire").gameObject.GetComponent<Button>();

		ShowHireMenu();
	}

	public void ShowBuildMenu(){
		buildMenu.alpha = 1.0f;
		buildMenu.interactable = true;
		buildMenu.blocksRaycasts = true;

		hireMenu.alpha = 0.0f;
		hireMenu.interactable = false;
		hireMenu.blocksRaycasts = false;

		buildButton.interactable = true;
		hireButton.interactable = false;
		buildButton.interactable = false;
		hireButton.interactable = true;
	}

	public void ShowHireMenu() {
		hireMenu.alpha = 1.0f;
		hireMenu.interactable = true;
		hireMenu.blocksRaycasts = true;

		buildMenu.alpha = 0.0f;
		buildMenu.interactable = false;
		buildMenu.blocksRaycasts = false;

		buildButton.interactable = true;
		hireButton.interactable = false;
	}
}
