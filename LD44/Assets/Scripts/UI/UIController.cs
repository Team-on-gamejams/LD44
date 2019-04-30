using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
	public TextMeshProUGUI meatText;

	void Update() {
		meatText.text  = $"Meat:  {GameManager.Instance.Player.CurrRes.meat}/{GameManager.Instance.Player.MaxRes.meat}";
	}
}
