using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Score : MonoBehaviour, ScoreCalculator {

	public int score() {
		if (Level.instance == null) {
			return 0;
		}
		Level level = Level.instance;
		int killed = level.killed_prisoners;

		switch (killed) {
		case 0:
			return 4;
		case 1:
			return 3;
		case 2:
			return 2;
		case 3:
			return 1;
		default:
			return 0;
		}
	}
}