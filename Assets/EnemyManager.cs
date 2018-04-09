using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime = 6f;
	public Transform[] spawnPoints;
	public int maxEnemys = 75;
	private int enemysSpawned = 0;
	private Player playerScript;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		playerScript = GetComponent<Player> ();
	}


	void Spawn () {
		if (enemysSpawned >= maxEnemys || playerScript.dead || (transform.position.x >= 77.5 && transform.position.x <= 86.5 && transform.position.z >= 91.5 && transform.position.z <= 100.5)) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemysSpawned++;
	}
}
