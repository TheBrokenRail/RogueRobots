using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		firstPersonCamera = player.GetComponentInChildren<Camera> ();
		playerMask = LayerMask.GetMask ("Player");
		texture = new Texture2D (1, 1, TextureFormat.ARGB32, false);
		playerScript = player.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
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
	}

	void OnGUI() {
		texture.SetPixel (0, 0, new Color(0, 0, 255, alpha));
		texture.Apply ();
		if (Input.GetButtonDown ("Cancel")) {
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
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				alpha = 0.35f;
				page = page - 1;
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				alpha = 0.35f;
				page = page + 1;
			}
			if (page < 0) {
				page = 0;
			}
			if (page > weapons.Length - 1) {
				page = weapons.Length - 1;
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
			if (Input.GetButtonDown ("Submit") && playerScript.GetSpareParts () >= weapon.itemWorth) {
				Instantiate (weapon, spawnPoint.position, Quaternion.Euler(Vector3.zero));
				playerScript.RemoveSpareParts (weapon.itemWorth);
			}
		}
	}
}
