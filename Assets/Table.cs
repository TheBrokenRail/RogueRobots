using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Table : MonoBehaviour {
	public GameObject player;
	private Player playerScript;
	private Camera firstPersonCamera;
	private float range = 10;
	private int playerMask;
	private bool open = false;
	private Texture2D texture;
	private float alpha = 0.5f;
	private int page = 0;
	public GameObject[] weapons;
	public Transform spawnPoint;
	private bool scrollLeft = false;
	private bool scrollRight = false;
	private bool submit = false;
	private bool cancel = false;
	private FirstPersonController controller;

	// Use this for initialization
	void Start () {
		firstPersonCamera = player.GetComponentInChildren<Camera> ();
		playerMask = LayerMask.GetMask ("Player");
		texture = new Texture2D (1, 1, TextureFormat.ARGB32, false);
		playerScript = player.GetComponent<Player> ();
		controller = player.GetComponent<FirstPersonController> ();
		controller.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		scrollLeft = Input.GetButtonDown ("ScrollLeft");
		scrollRight = Input.GetButtonDown ("ScrollRight");
		submit = Input.GetButtonDown ("Submit");
		cancel = Input.GetButtonDown ("Cancel");
		Ray clickRay = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
		RaycastHit clickHit;
		if (Physics.Raycast (clickRay, out clickHit, range, ~playerMask)) {
			Table item = clickHit.collider.GetComponent<Table> ();
			if (Input.GetButtonDown ("Fire2")) {
				if (item != null && item == this && !open) {
					page = 0;
					open = true;
				}
			}
			if (open && item == null && item != this) {
				open = false;
			}
		}
		if (open) {
			controller.enabled = false;
		} else {
			controller.enabled = true;
		}
	}

	void OnGUI() {
		texture.SetPixel (0, 0, new Color(0, 0, 255, alpha));
		texture.Apply ();
		if (cancel) {
			cancel = false;
			open = false;
		}
		if (open) {
			GUI.DrawTexture(new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), texture);
			if (alpha < 0.5f) {
				alpha = alpha + Time.deltaTime;
				if (alpha > 0.5f) {
					alpha = 0.5f;
				}
			}
			if (scrollLeft) {
				scrollLeft = false;
				alpha = 0.35f;
				page = page - 1;
			}
			if (scrollRight) {
				scrollRight = false;
				alpha = 0.35f;
				page = page + 1;
			}
			if (page < 0) {
				page = weapons.Length - 1;
			}
			if (page > weapons.Length - 1) {
				page = 0;
			}
			Item weapon = weapons [page].GetComponent<Item> ();
			string itemData = weapon.itemName + "\n" + weapon.damagePerShot + " Damage\n" + weapon.timeBetweenBullets + " Seconds Between Shots\nCosts " + weapon.itemWorth + " Spare Parts";
			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 24;
			Font font = (Font)Resources.Load ("arial", typeof(Font));
			style.font = font;
			GUI.Box (new Rect(Screen.width / 2, Screen.height / 2, 0, 0), itemData, style);
			if (submit) {
				submit = false;
				if (playerScript.GetSpareParts () >= weapon.itemWorth) {
					Instantiate (weapon, spawnPoint.position, Quaternion.Euler (Vector3.zero));
					playerScript.RemoveSpareParts (weapon.itemWorth);
					open = false;
				} else {
					alpha = 0.35f;
				}
			}
		}
	}
}
