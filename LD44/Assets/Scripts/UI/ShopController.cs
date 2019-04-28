using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
	GameObject buildMenu;
	GameObject hireMenu;
	Button buildButton;
	Button hireButton;

	void Start() {
		buildMenu = transform.Find("BuildPanel").gameObject;
		hireMenu = transform.Find("HirePanel").gameObject;
		buildButton = transform.Find("ButtonBuild").gameObject.GetComponent<Button>();
		hireButton = transform.Find("ButtonHire").gameObject.GetComponent<Button>();

		ShowBuildMenu();
	}

	public void ShowBuildMenu(){
		buildMenu.SetActive(true);
		hireMenu.SetActive(false);
		buildButton.interactable = true;
		hireButton.interactable = false;
		buildButton.interactable = false;
		hireButton.interactable = true;
	}

	public void ShowHireMenu() {
		hireMenu.SetActive(true);
		buildMenu.SetActive(false);
		buildButton.interactable = true;
		hireButton.interactable = false;
	}
}
