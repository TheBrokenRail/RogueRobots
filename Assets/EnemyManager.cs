using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime = 8f;
	public Transform[] spawnPoints;
	public int maxEnemys = 50;
	private int enemysSpawned = 0;
	private Player playerScript;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		playerScript = GetComponent<Player> ();
	}


	void Spawn () {
		if (enemysSpawned >= maxEnemys || playerScript.dead) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemysSpawned++;
	}
}
