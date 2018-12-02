using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
	public Color color;

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

	void OnDrawGizmosSelected() {
		Transform transform = this.GetComponent<Transform> ();
		Gizmos.DrawSphere (transform.position - this.center, 0.1f);
	}

}
	
	