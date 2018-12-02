using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphaScore : MonoBehaviour, ScoreCalculator {

	public int score() {
		if (Level.instance == null) {
			return 0;
		}
		var level = Level.instance;

		var total_prisoners = level.total_prisoners;
		var killed_prisoners = level.killed_prisoners;

		return total_prisoners - killed_prisoners;
	}
}
