using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool holding;
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
	private Vector3 hitNormal;
	private bool frontFacing = false;
	private float thirdPersonZoom = 0;
	public float zoomSpeed = 2f;

	// Use this for initialization
	void Start () {
		firstPersonCamera = GetComponentInChildren<Camera> ();
		Debug.Log (firstPersonCamera);
		holding = false;
		controller = GetComponent<CharacterController>();
		health = 20;
		score = 0;
		fallDistance = 0;
		lastY = transform.position.y;
		texture = new Texture2D (1, 1, TextureFormat.ARGB32, false);
		texture.SetPixel (0, 0, new Color(255, 0, 0, alpha));
		texture.Apply ();
		thirdPersonCamera.enabled = false;
		model.SetActive (false);
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
					model.SetActive (false);
				} else {
					frontFacing = true;
				}
			} else {
				frontFacing = false;
				thirdPerson = true;
				thirdPersonCamera.enabled = true;
				model.SetActive (true);
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
	}

	void OnGUI () {
		GUIStyle style = new GUIStyle ();
		style.normal.textColor = Color.white;
		style.fontSize = 24;
		Font font = (Font)Resources.Load ("arial", typeof(Font));
		style.font = font;
		if (!dead) {
			GUI.Box (new Rect (10, 10, 0, 0), "Health: " + health + "\nScore: " + score, style);
		} else {
			GUI.Box (new Rect (10, 10, 0, 0), "Score: " + score, style);
		}
		texture.SetPixel (0, 0, new Color(255, 0, 0, alpha));
		texture.Apply ();
		GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), texture);
		if (alpha > 0) {
			alpha = alpha - alpha * Time.deltaTime;
		}
		GUI.Box(new Rect(Screen.width / 2 - 15, Screen.height / 2, 40, 10), "");
		GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 15, 10, 40), "");
	}

	public void TakeDamage (int damage) {
		health = health - damage;
		if (!dead) {
			alpha = 0.5f;
		}
	}

	public void Score () {
		score = score + 1;
	}
}
