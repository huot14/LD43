using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NearbyTraps {

	static int _explore(int radius, Tile current, HashSet<Tile> visited) {
		int traps = 0;

		if(visited.Contains(current)) {
			return 0;
		}
		visited.Add (current);
		if (radius < 0) {
			return 0;
		}

		foreach (ValidMove candidate in current.candidateMoves()) {
			if (!visited.Contains (candidate.tile)) {
				traps += _explore (radius - 1, candidate.tile, visited);
			}
		}

		var maybeTrap = current.gameObject.GetComponent<Trap> ();
		if (maybeTrap != null && !maybeTrap.activated()) {
			traps++;
		}

		return traps;
	}

	/*The origin tile is included in the check*/
	public static int count(int radius, Tile origin) {
		HashSet<Tile> visited = new HashSet<Tile> ();
		return _explore (radius, origin, visited);
	}
}
