using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorScore : MonoBehaviour, ScoreCalculator {

	public int score() {
		if (Level.instance == null) {
			return 0;
		}

		var level = Level.instance;

		var total_prisoners = (float) level.total_prisoners;
		var killed_prisoners = (float) level.killed_prisoners;

		return (int) (((total_prisoners - killed_prisoners) / total_prisoners) * 3f);
	}
}
