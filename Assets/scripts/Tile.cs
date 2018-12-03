using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public interface OnStep {
	
}

public enum MovementType {
	RIGHT,
	DOWN,
	LEFT,
	UP,
	TELEPORT,
}



public struct ValidMove {
	public Tile tile;
	public MovementType type;
}

[System.Serializable]
public class Tile : MonoBehaviour {


	public Tile right;
	public Tile down;
	public Tile left;
	public Tile up;

	/*Relative to the current position*/
	public Vector3 center;

	public HashSet<ValidMove> candidateMoves() {
		HashSet<ValidMove> set = new HashSet<ValidMove>();

		if(this.right != null) {
			set.Add(new ValidMove { tile = this.right, type = MovementType.RIGHT});
		}

		if(this.down != null) {
			set.Add(new ValidMove { tile = this.down, type = MovementType.DOWN});
		}

		if(this.left != null) {
			set.Add(new ValidMove { tile = this.left, type = MovementType.LEFT});
		}

		if(this.up != null) {
			set.Add(new ValidMove { tile = this.up, type = MovementType.UP});
		}

		var teleporter = this.GetComponent<Teleporter> ();
		if(teleporter != null) {
			var destination = teleporter.destination ();
			if (destination == null) {
				Debug.Log ("Teleporter on tile " + this.name + " has a null destination");
			} else {
				set.Add(new ValidMove { tile = destination , type = MovementType.TELEPORT});
			}

		}

		return set;
	}

	#if UNITY_EDITOR
	void OnDrawGizmosSelected() {
		Transform transform = this.GetComponent<Transform> ();
		Gizmos.DrawSphere (transform.position - this.center, 0.1f);

		foreach (ValidMove candidate in this.candidateMoves()) {
			Handles.color = Color.cyan;
			Handles.DrawLine (this.transform.position, candidate.tile.transform.position);
		}

		//Handles.color = Color.blue;
		Gizmos.color = Color.red;
		if (this.GetComponents<Trap>().Length > 0) {
			Vector3 _from = this.transform.position;
			_from.x = _from.x - 0.25f;
			_from.z = _from.z - 0.25f;
			Vector3 _to = this.transform.position;
			_to.x = _to.x + 0.25f;
			_to.z = _to.z + 0.25f;
			Gizmos.DrawLine (_from, _to);

			_from = this.transform.position;
			_from.x = _from.x - 0.25f;
			_from.z = _from.z + 0.25f;
			_to = this.transform.position;
			_to.x = _to.x + 0.25f;
			_to.z = _to.z - 0.25f;
			Gizmos.DrawLine (_from, _to);
		}
	}
	#endif

}
	
	