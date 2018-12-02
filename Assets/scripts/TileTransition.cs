using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTransition : MonoBehaviour {

	public ColorOption[] allowed;

	public bool isAllowed(ColorOption color) {
		bool contains = false;

		foreach (var allowed in this.allowed) {
			if (allowed.color == color.color) {
				contains = true;
				break;
			}
		}

		return contains;
	}
}
