using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Trap {
	bool activated();
}

[RequireComponent(typeof(Tile))]
public class BasicTrap : MonoBehaviour {
	bool _activated = false;

	public bool activated() {
		return this._activated;
	}
}
