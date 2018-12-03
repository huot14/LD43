using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingBasedText : MonoBehaviour, EndLevelText {

	public string failure = "";
	public string stars1 = "";
	public string stars2 = "";
	public string stars3 = "";

	public string message() {
		Level level = Level.instance;
		if (level == null) {
			return "";
		}

		int score = level.score;
		switch(score) {
		case 0:
			return failure;
		case 1:
			return stars1;
		case 2:
			return stars2;
		case 3:
			return stars3;
		default:
			return "";
		}
	}

}
