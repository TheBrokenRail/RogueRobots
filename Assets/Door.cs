using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueRobots {
	public class Door : MonoBehaviour {
		private bool open = false;
		private float angle = 0;
		public Transform pivotPoint;
		private float range = 10;
		
		// Update is called once per frame
		void Update () {
			if (open && angle < 90) {
				float diff = Time.deltaTime * 200;
				if (diff + angle > 90) {
					diff = 90 - angle;
				}
				transform.RotateAround (pivotPoint.position, Vector3.up, diff);
				angle = angle + diff;
			}
			if (!open && angle > 0) {
				float diff = Time.deltaTime * 200;
				if (angle - diff < 0) {
					diff = angle;
				}
				transform.RotateAround (pivotPoint.position, Vector3.up, -diff);
				angle = angle - diff;
			}
		}

		public void Select () {
			if (open) {
				open = false;
			} else {
				open = true;
			}
		}
	}
}