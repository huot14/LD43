using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGroup))]
public class TileGroupEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		TileGroup tiles = (TileGroup)target;
		var swapLeftRight = GUILayout.Button ("Swap left and right");
		var swapUpDown = GUILayout.Button ("Swap up and down");


		if (swapLeftRight) {
			tiles.swapLeftRight ();
		}

		if (swapUpDown) {
			tiles.swapUpDown ();
		}

	}

}