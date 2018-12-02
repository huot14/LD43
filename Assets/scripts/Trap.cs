﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Trap {
	bool activated();
	void activate();
	
}

[RequireComponent(typeof(Tile))]
public class BasicTrap : MonoBehaviour {
	bool _activated = false;

	public bool activated() {
		return this._activated;
	}

	public void activate() {
		if (!this._activated) {
			Debug.Log ("A trap was triggered!");
			Level.instance.traps_triggered++;
			this._activated = true;
		}


	}
}
