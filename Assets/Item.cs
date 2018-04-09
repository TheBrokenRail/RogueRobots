using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	public GameObject player;
	public GameObject particleEffect;
	private Rigidbody rigidBody;
	private Player playerScript;
	private bool held;
	public int damagePerShot = 5;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;
	private float timer;
	private int shootableMask;
	private int playerMask;
	private LineRenderer gunLine;
	private AudioSource gunAudio;
	private Light gunLight;
	private float effectsDisplayTime = 0.05f;
	private Camera firstPersonCamera;
	public Color lineColor = Color.green;
	public string itemName = "Laser Gun";
	private float overheatChance = 0.1f;
	public float temperture = 20;
	public int itemWorth = 6;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		timer = timeBetweenBullets;
		held = false;
		rigidBody = GetComponent<Rigidbody>();
		playerScript = player.GetComponent<Player>();
		shootableMask = LayerMask.GetMask ("NotShootable");
		playerMask = LayerMask.GetMask ("Player");
		// Set up the references.
		gunLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		firstPersonCamera = player.GetComponentInChildren<Camera> ();
		DisableEffects ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player) {
			return;
		}
		Ray clickRay = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
		RaycastHit clickHit;
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast (clickRay, out clickHit, range, ~playerMask)) {
				Item item = clickHit.collider.GetComponent<Item> ();
				if (item != null && item == this) {
					if (held) {
						held = false;
						playerScript.holding = null;
					} else if (!playerScript.holding) {
						held = true;
						playerScript.holding = this;
					}
				}
			}
		}
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= effectsDisplayTime) {
			DisableEffects ();
		}
		if (held) {
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;
			Vector3 playerPos = player.transform.position;
			Vector3 playerDirection = player.transform.forward;
			Quaternion playerRotation = player.transform.rotation;
			float spawnDistance = 1.25f;
			transform.position = playerPos + playerDirection * spawnDistance;
			transform.rotation = player.transform.rotation;
			timer += Time.deltaTime;
			if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets) {
				Shoot ();
			}
		} else {
			rigidBody.useGravity = true;
		}
		if (temperture > 20) {
			temperture = temperture - Time.deltaTime * 10;
			if (temperture < 20) {
				temperture = 20;
			}
		}
	}

	public void Burn () {
		playerScript.AddSpareParts (itemWorth);
		Destroy (gameObject);
	}

	public bool GetHeld() {
		return held;
	}

	public void DisableEffects () {
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	void Shoot () {
		timer = 0f;
		if (itemName.StartsWith("Expiremental") && overheatChance >= Random.value) {
			temperture = 80;
		}
		if (temperture > 20) {
			playerScript.WarnTint ();
			return;
		}
		gunAudio.Play ();
		gunLight.enabled = true;
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		gunLine.material.color = lineColor;
		Ray shootRay = new Ray(transform.position, transform.forward);
		RaycastHit shootHit;
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		if (Physics.Raycast (shootRay, out shootHit, range, ~shootableMask)) {
			if (particleEffect != null) {
				Instantiate (particleEffect, shootHit.point, Quaternion.Euler(Vector3.zero));
			}
			Enemy enemy = shootHit.collider.GetComponent<Enemy>();
			if (enemy != null) {
				enemy.TakeDamage (damagePerShot);
			}
			gunLine.SetPosition (1, shootHit.point);
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
