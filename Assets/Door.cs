using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject player;
	private Camera firstPersonCamera;
	private bool open = false;
	private float angle = 0;
	public Transform pivotPoint;
	private float range = 10;
	private int playerMask;

	// Use this for initialization
	void Start () {
		firstPersonCamera = player.GetComponentInChildren<Camera> ();
		playerMask = LayerMask.GetMask ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Ray clickRay = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
		RaycastHit clickHit;
		if (Input.GetButtonDown ("Fire2")) {
			if (Physics.Raycast (clickRay, out clickHit, range, ~playerMask)) {
				Door item = clickHit.collider.GetComponent<Door> ();
				if (item != null && item == this) {
					if (open) {
						open = false;
					} else {
						open = true;
					}
				}
			}
		}
		if (open && angle < 90) {
			float diff = Time.deltaTime * 200;
			if (diff + angle > 90) {
				diff = diff + angle - 90;
			}
			transform.RotateAround (pivotPoint.position, Vector3.up, diff);
			angle = angle + diff;
		}
		if (!open && angle > 0) {
			float diff = Time.deltaTime * 200;
			if (angle - diff < 0) {
				diff = (angle - diff) * -1;
			}
			transform.RotateAround (pivotPoint.position, Vector3.up, -diff);
			angle = angle - diff;
		}
	}
}
