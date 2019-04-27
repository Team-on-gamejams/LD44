using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionComponent : MonoBehaviour {
	bool isSelecting = false;
	Vector3 mousePosition1;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			isSelecting = true;
			mousePosition1 = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0)) {
			var player = GameManager.Instance.Player;
			player.CleatSelectedUnits();
			foreach (var unit in player.Units) 
				if(IsWithinSelectionBounds(unit.gameObject))
					player.AddSelectedUnit(unit);
			isSelecting = false;
		}
	}

	void OnGUI() {
		if (isSelecting) {
			var rect = GetScreenRect(mousePosition1, Input.mousePosition);
			DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
			DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
		}
	}

	public bool IsWithinSelectionBounds(GameObject gameObject) {
		if (!isSelecting)
			return false;

		var camera = Camera.main;
		var viewportBounds =
			GetViewportBounds(camera, mousePosition1, Input.mousePosition);

		return viewportBounds.Contains(
			camera.WorldToViewportPoint(gameObject.transform.position));
	}

	static Texture2D _whiteTexture;
	public static Texture2D WhiteTexture {
		get {
			if (_whiteTexture == null) {
				_whiteTexture = new Texture2D(1, 1);
				_whiteTexture.SetPixel(0, 0, Color.white);
				_whiteTexture.Apply();
			}

			return _whiteTexture;
		}
	}

	public static void DrawScreenRect(Rect rect, Color color) {
		GUI.color = color;
		GUI.DrawTexture(rect, WhiteTexture);
		GUI.color = Color.white;
	}

	public static void DrawScreenRectBorder(Rect rect, float thickness, Color color) {
		// Top
		DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
		// Left
		DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
		// Right
		DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
		// Bottom
		DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
	}

	public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2) {
		screenPosition1.y = Screen.height - screenPosition1.y;
		screenPosition2.y = Screen.height - screenPosition2.y;
		
		var topLeft = Vector3.Min(screenPosition1, screenPosition2);
		var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
		
		return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
	}

	public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2) {
		var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
		var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
		var min = Vector3.Min(v1, v2);
		var max = Vector3.Max(v1, v2);
		min.z = camera.nearClipPlane;
		max.z = camera.farClipPlane;

		var bounds = new Bounds();
		bounds.SetMinMax(min, max);
		return bounds;
	}
}
