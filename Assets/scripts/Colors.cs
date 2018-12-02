using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorOption {
	public TileColors _known;
	public Color _custom;

	public Color color {
		get {
			if (_known == TileColors.CUSTOM) {
				return _custom;
			} else {
				return TileColorsMap.fromEnum(_known);
			}
		}
	}
}

public enum TileColors {
	WHITE,
	RED,
	GREEN,
	BLUE,
	CUSTOM,
}

public static class TileColorsMap {
	public static Color red = Color.red;
	public static Color green = Color.green;
	public static Color blue = Color.blue;
	public static Color white = Color.white;

	public static Color fromEnum(TileColors name) {
		switch (name) {
		case TileColors.WHITE:
				return TileColorsMap.white;
		case TileColors.RED:
			return  TileColorsMap.red;
		case TileColors.GREEN:
			return TileColorsMap.green;
		case TileColors.BLUE:
			return  TileColorsMap.blue;
		default:
			return  Color.white;
		}
	}

}