using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OnStep {
	
}

[System.Serializable]
public class Tile : MonoBehaviour {

	public Tile right;
	public Tile down;
	public Tile left;
	public Tile up;

	/*Relative to the current position*/
	public Vector3 center;
	public Color color;

	void OnDrawGizmosSelected() {
		Transform transform = this.GetComponent<Transform> ();
		Gizmos.DrawSphere (transform.position - this.center, 0.1f);
	}

}