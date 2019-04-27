using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour {
	public BloodConsumper bloodConsumper;
	public Health health;
	public Price price;

	GameObject selection;

	Pathfinder pathfinder;
	int pathPos;
	Vector2[] path;
	public float speed = 5;
	public bool isMoving;
	public bool isReachDestination;

	void Start() {
		isMoving = false;
		isReachDestination = false;
		selection = transform.Find("Selection").gameObject;

		LeanTween.delayedCall(1, () => {
			pathfinder = new Pathfinder(GameManager.Instance.Player.aINavMeshGenerator);
			GameManager.Instance.Player.AddUnit(this);
		});
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		bloodConsumper.Tick(Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Unit"){
			UnitBase unit = collision.gameObject.GetComponent<UnitBase>();
			if(unit.isReachDestination){
				isMoving = false;
			}
		}
	}

	public void Select(){
		selection.SetActive(true);
	}

	public void UnSelect() {
		selection.SetActive(false);
	}

	public void MoveTo(Vector2 pos){
		MoveTo(pos, true);
	}

	int tryToMoveCnt = 0;
	void MoveTo(Vector2 pos, bool callByUser) {
		if (isMoving)
			LeanTween.cancel(gameObject, false);

		if(callByUser)
			tryToMoveCnt = 0;

		path = pathfinder.FindPath(transform.position, pos);
		if (path != null) {
			isMoving = true;
			isReachDestination = false;
			pathPos = 0;
			Move();
		}
		else {
			++tryToMoveCnt;
			if (tryToMoveCnt != 10) {
				LeanTween.delayedCall(gameObject, 0.5f, () => {
					MoveTo(pos, false);
				});
			}
		}
	}

	void Move(){
		if (path == null || pathPos == path.Length || !isMoving) {
			isMoving = false;
			isReachDestination = true;
			return;
		}

		Vector3 targetDir = path[pathPos] - (Vector2)transform.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		var initRot = transform.rotation;
		var endRot = Quaternion.AngleAxis(angle, Vector3.forward);

		LeanTween.value(gameObject, 0, 1, 0.1f)
		.setOnUpdate((float val) => {
			transform.rotation = Quaternion.Lerp(initRot, endRot, val);
		});

		transform.LeanMove(path[pathPos], (((Vector2)(transform.position)) - path[pathPos]).magnitude / speed)
		.setOnComplete(()=> {
			++pathPos;
			Move();
		});
	}

	public void RecalcMoveIfStop(){
		if(!isMoving){
			isReachDestination = false;
		}
	}
}
