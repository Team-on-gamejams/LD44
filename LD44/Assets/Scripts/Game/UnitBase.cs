using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : BattleUnit {
	public BloodConsumper bloodConsumper;
	public Health health;
	public Price price;

	SpriteRenderer selection;

	Pathfinder pathfinder;
	int pathPos;
	Vector2[] path;
	public float speed = 5;
	internal bool isReachDestination;

	public BuildingType awaliableAfter = BuildingType.None;
	public float buildTime = 1.0f;

	LTDescr moveDesc = null, rotateDesc = null, repeatDesc = null;

	void Start() {
		isMoving = false;
		isReachDestination = false;
		selection = transform.Find("Selection").gameObject.GetComponent<SpriteRenderer>();

		health.Init();

		if(GameManager.Instance.Player != null && GameManager.Instance.Player.Buildings.Count != 0) {
			pathfinder = new Pathfinder(GameManager.Instance.Player.aINavMeshGenerator);
			GameManager.Instance.Player.AddUnit(this);
		}
		else{
			LeanTween.delayedCall(1, () => {
				pathfinder = new Pathfinder(GameManager.Instance.Player.aINavMeshGenerator);
				GameManager.Instance.Player.AddUnit(this);
			});
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Unit") {
			UnitBase unit = collision.gameObject.GetComponent<UnitBase>();
			if (unit.isReachDestination) {
				isMoving = false;
			}
		}
	}

	public override void Die(){
		GameManager.Instance.Player.RemoveUnit(this);
		Destroy(gameObject);
	}

	public void Select() {
		SetAlpha(selection, 1.0f);
	}

	public void UnSelect() {
		SetAlpha(selection, 0.0f);
	}

	public override void MoveTo(Vector2 pos) {
		isMovingByPlayer = true;
		MoveTo(pos, true);
	}

	int tryToMoveCnt = 0;
	void MoveTo(Vector2 pos, bool callByUser) {
		if (isMoving) {
			if(moveDesc != null){
				LeanTween.cancel(moveDesc.id);
				moveDesc = null;
			}
			if (rotateDesc != null) {
				LeanTween.cancel(rotateDesc.id);
				rotateDesc = null;
			}
			if (repeatDesc != null) {
				LeanTween.cancel(repeatDesc.id);
				repeatDesc = null;
			}
			isMoving = false;
		}

		if (callByUser)
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
				repeatDesc = LeanTween.delayedCall(gameObject, 0.3f, () => {
					MoveTo(pos, false);
				});
			}
		}
	}

	void Move() {
		if (path == null || pathPos == path.Length || !isMoving) {
			isMovingByPlayer = isMoving = false;
			isReachDestination = true;
			return;
		}

		Vector3 targetDir = path[pathPos] - (Vector2)transform.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		var initRot = transform.rotation;
		var endRot = Quaternion.AngleAxis(angle, Vector3.forward);

		rotateDesc = LeanTween.value(gameObject, 0, 1, 0.1f)
		.setOnUpdate((float val) => {
			transform.rotation = Quaternion.Lerp(initRot, endRot, val);
		});

		moveDesc = transform.LeanMove(path[pathPos], (((Vector2)(transform.position)) - path[pathPos]).magnitude / speed)
		.setOnComplete(() => {
			++pathPos;
			Move();
		});
	}

	public void RecalcMoveIfStop() {
		if (!isMoving) {
			isReachDestination = false;
		}
	}

	void SetAlpha(SpriteRenderer image, float a) {
		Color color = image.color;
		color.a = a;
		image.color = color;
	}
}
