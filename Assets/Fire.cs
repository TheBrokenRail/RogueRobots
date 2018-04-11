using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueRobots {
	public class Fire : MonoBehaviour {
		void OnTriggerEnter(Collider other) {
			Item item = other.gameObject.GetComponent<Item> ();
			if (item != null) {
				if (!item.GetHeld ()) {
					item.Burn ();
				}
			}
		}
	}
}