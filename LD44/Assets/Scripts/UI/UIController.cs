using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
	public TextMeshProUGUI bloodText;
	public TextMeshProUGUI meatText;

	void Update() {
		bloodText.text = $"Blood: {GameManager.Instance.Player.CurrRes.blood}/{GameManager.Instance.Player.MaxRes.blood}";
		meatText.text  = $"Meat:  {GameManager.Instance.Player.CurrRes.meat}/{GameManager.Instance.Player.MaxRes.meat}";
	}
}
