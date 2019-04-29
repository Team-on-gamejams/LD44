using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public List<GameObject> enemies;
	public float spawnTime = 1.0f;
	float currTime = 0.0f;

	public float minX, maxX, minY, maxY;

	void Start() {
		SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
		maxX = sp.size.x / 2 * sp.transform.localScale.x;
		minX = -maxX;
		maxY = sp.size.y / 2 * sp.transform.localScale.y;
		minY = -maxY;
	}

	void Update() {
		currTime += Time.deltaTime;
		while(currTime >= spawnTime){
			currTime -= spawnTime;
			if(Random.Range(0, 2) == 1){
				Instantiate(
					enemies[Random.Range(0, enemies.Count)],
					new Vector3(Random.Range(0, 2) == 1 ? minX : maxX, Random.Range(minY, maxY), 0),
					new Quaternion()
				);
			}
			else {
				Instantiate(
					enemies[Random.Range(0, enemies.Count)],
					new Vector3(Random.Range(minX, maxX), Random.Range(0, 2) == 1 ? minY : maxY, 0),
					new Quaternion()
				);
			}
		}
	}
}
