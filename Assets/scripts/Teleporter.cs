﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Teleporter {
	Tile destination ();
}

[RequireComponent(typeof(Tile))]
public class StaticTeleporter : MonoBehaviour, Teleporter {

	public Tile connected_to;

	public Tile destination() {
		return this.connected_to;
	}
}
