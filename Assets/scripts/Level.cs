using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour {
	public Tile start;

	public void Start() {
		Debug.Log ("Start is " + start.name);
	}
}
