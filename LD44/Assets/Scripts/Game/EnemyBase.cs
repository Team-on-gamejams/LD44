using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BattleUnit {
	public Health health;

	Pathfinder pathfinder;
	int pathPos;
	Vector2[] path;
	public float speed = 5;
	bool isMoving;

	int buildId;

	void Start() {
		health.Init();
		
		LeanTween.delayedCall(1.5f, () => {
			pathfinder = new Pathfinder(GameManager.Instance.Player.aINavMeshGenerator);
			//TryMoveToHearth();
		});
	}

	public override void Die() {
		Destroy(gameObject);
	}

	public void TryMoveToHearth(){
		buildId = 0;
		MoveTo(GameManager.Instance.Player.Buildings[buildId].transform.position);
	}

	public override void MoveTo(Vector2 pos) {
		MoveTo(pos, true);
	}

	int tryToMoveCnt = 0;
	void MoveTo(Vector2 pos, bool callByUser) {
		if (isMoving)
			LeanTween.cancel(gameObject, false);

		if (callByUser)
			tryToMoveCnt = 0;

		path = pathfinder.FindPath(transform.position, pos);
		if (path != null) {
			isMoving = true;
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

	void Move() {
		if (path == null || pathPos == path.Length || !isMoving) {
			isMoving = false;
			return;
		}

		if ((((Vector2)(transform.position)) - path[pathPos]).x <= 0)
			transform.rotation = Quaternion.Euler(0, 0, 0);
		else
			transform.rotation = Quaternion.Euler(0, 180, 0);


		transform.LeanMove(path[pathPos], (((Vector2)(transform.position)) - path[pathPos]).magnitude / speed)
		.setOnComplete(() => {
			++pathPos;
			Move();
		});
	}

	void SetAlpha(SpriteRenderer image, float a) {
		Color color = image.color;
		color.a = a;
		image.color = color;
	}

	void Awake() {
		EventManager.AddOrRemoveBuildingsEvent += AddOrRemoveBuildingsEvent;
	}

	void OnDestroy() {
		EventManager.AddOrRemoveBuildingsEvent -= AddOrRemoveBuildingsEvent;
	}

	void AddOrRemoveBuildingsEvent(EventData ed) {
		if(pathfinder != null)
			TryMoveToHearth();
	}
}
