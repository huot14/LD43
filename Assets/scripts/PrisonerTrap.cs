using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class PrisonerTrap : MonoBehaviour, Trap {
	bool _activated = false;

    public Prisoner prisoner;
    public GameObject trapMarkerPrefab;

	public bool activated() {
		return this._activated;
	}

	public void activate() {
		if (!this._activated) {
			Debug.Log ("A trap was triggered!");
			Level.instance.traps_triggered++;
			this._activated = true;

            prisoner.kill();

            markTrap();
		}
	}

    //Visually indicate to player they have stepped on a trap
    void markTrap()
    {
        GameObject trapMarker = GameObject.Instantiate(trapMarkerPrefab);
        Vector3 position = gameObject.transform.position;
        position.y += 0.14f;
        trapMarker.transform.position = position;
    }
}