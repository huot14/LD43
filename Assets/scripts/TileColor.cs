using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour {

	public ColorOption color;

	void updateMesh() {
		Transform top = transform.GetChild (1);
		MeshRenderer renderer = top.GetComponent<MeshRenderer> ();
		renderer.material.color = this.color.color;
	}

	void Start() {
		updateMesh ();
	}

	void OnValidate() {
		updateMesh ();
	}
}
