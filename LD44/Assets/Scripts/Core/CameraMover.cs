using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	public float speed;
	public SpriteRenderer border;
	Camera camera;

	void Start() {
		camera = GetComponent<Camera>();
	}

	void Update() {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			camera.transform.LeanMoveY(Mathf.Min(transform.position.y + speed * Time.deltaTime, border.bounds.max.y), Time.deltaTime);
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			camera.transform.LeanMoveY(Mathf.Max(transform.position.y - speed * Time.deltaTime, border.bounds.min.y), Time.deltaTime);
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			camera.transform.LeanMoveX(Mathf.Max(transform.position.x - speed * Time.deltaTime, border.bounds.min.x), Time.deltaTime);
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			camera.transform.LeanMoveX(Mathf.Min(transform.position.x + speed * Time.deltaTime, border.bounds.max.x), Time.deltaTime);
	}
}
