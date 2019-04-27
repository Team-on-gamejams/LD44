using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	// guarantee this will be always a singleton only - can't use the constructor!
	protected GameManager() { }

	public bool IsTimeStop {
		set {
			isTimeStop = value;
			EventManager.CallOnTimeStopChangedEvent();
		}
		get => isTimeStop;
	}
	private bool isTimeStop;

	public EventManager EventManager;
	public PlayerController Player;

	public void Start() {
		EventManager = new EventManager();
		Input.multiTouchEnabled = false;
		LeanTween.init(800);

		IsTimeStop = true;
	}
}