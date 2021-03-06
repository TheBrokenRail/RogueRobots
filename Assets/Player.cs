﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace RogueRobots {
	public class Player : MonoBehaviour {
		public Item holding;
		private CharacterController controller;
		private int health;
		private float fallDistance;
		private float lastY;
		private int score;
		public bool dead = false;
		private Texture2D texture;
		private float alpha = 0;
		private Camera firstPersonCamera;
		public Camera thirdPersonCamera;
		private bool thirdPerson;
		public GameObject model;
		private SkinnedMeshRenderer[] modelComponents;
		private Vector3 hitNormal;
		private bool frontFacing = false;
		private float thirdPersonZoom = 0;
		public float zoomSpeed = 2f;
		private float regenerationPoints = 0;
		private int tintColor = 0;
		private int spareParts = 0;
		private float range = 100;
		private int playerMask;

		// Use this for initialization
		void Start () {
			firstPersonCamera = GetComponentInChildren<Camera> ();
			Debug.Log (firstPersonCamera);
			controller = GetComponent<CharacterController>();
			health = 20;
			score = 0;
			fallDistance = 0;
			lastY = transform.position.y;
			texture = new Texture2D (1, 1, TextureFormat.ARGB32, false);
			texture.SetPixel (0, 0, new Color(255, 0, 0, alpha));
			texture.Apply ();
			thirdPersonCamera.enabled = false;
			modelComponents = model.GetComponentsInChildren<SkinnedMeshRenderer> ();
			foreach (SkinnedMeshRenderer component in modelComponents) {
				component.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
			playerMask = LayerMask.GetMask ("Player");
		}

		// Update is called once per frame
		void Update () {
			Vector3 cameraPos = firstPersonCamera.transform.position;
			Vector3 cameraDirection = firstPersonCamera.transform.forward;
			Quaternion cameraRotation = firstPersonCamera.transform.rotation;
			if (Input.GetButton ("ThirdPersonZoomIn")) {
				thirdPersonZoom = thirdPersonZoom - Time.deltaTime * zoomSpeed;
				if (thirdPersonZoom < 0) {
					thirdPersonZoom = 0;
				}
			}
			if (Input.GetButton ("ThirdPersonZoomOut")) {
				thirdPersonZoom = thirdPersonZoom + Time.deltaTime * zoomSpeed;
				if (thirdPersonZoom > 4) {
					thirdPersonZoom = 4;
				}
			}
			float spawnDistance = -1.5f - thirdPersonZoom;
			if (frontFacing) {
				spawnDistance = -spawnDistance;
				cameraRotation = Quaternion.Euler(new Vector3(cameraRotation.eulerAngles.x + 180, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z + 180));
			}
			if (thirdPerson) {
				thirdPersonCamera.transform.position = cameraPos + cameraDirection * spawnDistance;
				thirdPersonCamera.transform.rotation = cameraRotation;
			}
			if (Input.GetButtonDown ("ThirdPerson")) {
				if (thirdPerson) {
					if (frontFacing) {
						thirdPerson = false;
						thirdPersonCamera.enabled = false;
						foreach (SkinnedMeshRenderer component in modelComponents) {
							component.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
					} else {
						frontFacing = true;
					}
				} else {
					frontFacing = false;
					thirdPerson = true;
					thirdPersonCamera.enabled = true;
					foreach (SkinnedMeshRenderer component in modelComponents) {
						component.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
					}
				}
			}
			if (!controller.isGrounded) {
				fallDistance = fallDistance + (lastY - transform.position.y);
			} else {
				if (fallDistance > 2) {
					float fallDamage = fallDistance - 2;
					TakeDamage((int)fallDamage);
				}
				fallDistance = 0;
			}
			lastY = transform.position.y;
			if (health < 1 && !dead) {
				dead = true;
				transform.position = new Vector3 (204, 50, 178);
			}
			if (transform.position.y < -5) {
				TakeDamage (20);
			}
			if (regenerationPoints >= 100) {
				Regenerate (5);
				regenerationPoints = 0;
			}
			if (Input.GetButtonDown("RestartGame") && dead) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			Ray clickRay = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
			RaycastHit clickHit;
			if (Input.GetButtonDown ("Fire2")) {
				if (Physics.Raycast (clickRay, out clickHit, range, ~playerMask)) {
					Item item = clickHit.collider.GetComponent<Item> ();
					Door door = clickHit.collider.GetComponent<Door> ();
					Table table = clickHit.collider.GetComponent<Table> ();
					if (item) {
						item.Select ();
					} else if (door) {
						door.Select ();
					} else if (table) {
						table.Select ();
					}
				}
			}
		}

		void OnGUI () {
			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.white;
			style.fontSize = 24;
			Font font = (Font)Resources.Load ("arial", typeof(Font));
			style.font = font;
			string itemData = "";
			if (holding != null) {
				itemData = "\n\n" + holding.itemName + "\n" + holding.damagePerShot + " Damage\n" + holding.timeBetweenBullets + " Seconds Between Shots\n" + System.Math.Round(holding.temperture, 1) + "\u00B0 Celsius";
			}
			if (!dead) {
				GUI.Box (new Rect (10, 10, 0, 0), "Health: " + health + "\nRegeneration Points: " + regenerationPoints + "\nScore: " + score + "\n" + spareParts + " Spare Parts" + itemData, style);
			} else {
				GUI.Box (new Rect (10, 10, 0, 0), "Score: " + score + "\n" + spareParts + " Spare Parts" + itemData, style);
			}
			if (tintColor == 0) {
				texture.SetPixel (0, 0, new Color (255, 0, 0, alpha));
			} else if (tintColor == 1) {
				texture.SetPixel (0, 0, new Color (0, 255, 0, alpha));
			} else if (tintColor == 2) {
				texture.SetPixel (0, 0, new Color (255, 255, 0, alpha));
			}
			texture.Apply ();
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), texture);
			if (alpha > 0) {
				alpha = alpha - alpha * Time.deltaTime;
			}
			GUI.Box(new Rect(Screen.width / 2 - 15, Screen.height / 2, 40, 10), "");
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 15, 10, 40), "");
		}

		public void TakeDamage (int damage) {
			regenerationPoints = 0;
			health = health - damage;
			if (!dead) {
				tintColor = 0;
				alpha = 0.5f;
			}
		}

		public void WarnTint () {
			if (!dead) {
				tintColor = 2;
				alpha = 0.5f;
			}
		}

		private void Regenerate (int amount) {
			health = health + amount;
			if (health > 20) {
				health = 20;
			}
			if (!dead) {
				tintColor = 1;
				alpha = 0.5f;
			}
		}

		public void AddRegenerationPoints (float amount) {
			regenerationPoints = regenerationPoints + amount;
		}

		public void Score () {
			score = score + 1;
		}

		public void AddSpareParts (int amount) {
			spareParts = spareParts + amount;
		}

		public int GetSpareParts() {
			return spareParts;
		}

		public void RemoveSpareParts(int amount) {
			spareParts = spareParts - amount;
		}
	}
}