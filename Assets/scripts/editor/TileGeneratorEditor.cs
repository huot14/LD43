using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		TileGenerator generator = (TileGenerator)target;
		var button_build = GUILayout.Button ("Build");


		if (button_build) {
			generator.build();
		}

	}

}