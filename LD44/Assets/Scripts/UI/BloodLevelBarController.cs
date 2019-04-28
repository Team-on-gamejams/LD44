using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodLevelBarController : MonoBehaviour {
	public Image fillImage;
	public RectTransform wave;

	void Awake() {
		EventManager.BloodLevelChangedEvent += BloodLevelChangedEvent;
	}

	void OnDestroy() {
		EventManager.BloodLevelChangedEvent -= BloodLevelChangedEvent;
	}

	void BloodLevelChangedEvent(EventData ed){
		ChangeFill();
	}

	void ChangeFill(){
		LeanTween.cancel(gameObject, false);
		LeanTween.value(gameObject, fillImage.fillAmount, 1 - (GameManager.Instance.Player.bloodTake / GameManager.Instance.Player.bloodProd), 1.0f)
		.setOnUpdate((float a)=> { 
			fillImage.fillAmount = a;
			wave.position = new Vector3(wave.position.x, fillImage.rectTransform.rect.height * a, wave.position.z);
		});
	}
}
