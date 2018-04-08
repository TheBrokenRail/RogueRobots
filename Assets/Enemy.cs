using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {
	private int health;
	public GameObject player;
	private Player playerScript;
	private float timer;
	private bool playerInRange;
	public int attackDamage = 2;
	public float timeBetweenAttacks = 0.5f;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		health = 20;
		playerScript = player.GetComponent<Player>();
		AICharacterControl control = GetComponent<AICharacterControl> ();
		control.target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange && health > 0) {
			timer = 0f;
			playerScript.TakeDamage (attackDamage);
		}
		if (health < 1) {
			playerScript.Score ();
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		if (playerScript.dead) {
			playerScript.AddKillStreak ();
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision col) {
		if(col.gameObject == player) {
			playerInRange = true;
		}
	}

	void OnCollisionExit (Collision col) {
		if(col.gameObject == player) {
			playerInRange = false;
		}
	}

	public void TakeDamage (int damage) {
		health = health - damage;
	}
}
