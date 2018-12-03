using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroup : MonoBehaviour {

	public void swapLeftRight() {
		foreach (Tile tile in this.GetComponentsInChildren<Tile>()) {
			var temp = tile.right;
			tile.right = tile.left;
			tile.left = temp;
		}
	}

	public void swapUpDown() {
		foreach (Tile tile in this.GetComponentsInChildren<Tile>()) {
			var temp = tile.up;
			tile.up = tile.down;
			tile.down = temp;
		}
	}
}
