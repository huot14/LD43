using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileGenerator : MonoBehaviour {

    // n*m grid of tiles
    public int rows;
    public int columns;
	public bool connected = true;

    public float separationMultiplier;

    public GameObject tilePrefab;

	public void build() {
		GameObject parent = new GameObject ();
		GameObject[] tiles = new GameObject[rows * columns];
		parent.name = "Tiles";

		for (int z = 0; z < rows; z++)
		{
			for (int x = 0; x < columns; x++)
			{
				GameObject tile = GameObject.Instantiate(tilePrefab);
				tiles [(z * columns) + x] = tile;
				Debug.Log ((z * columns) + x);
				tile.transform.SetParent (parent.transform);
				tile.transform.position = new Vector3(x * separationMultiplier, 0, z * separationMultiplier);
                tile.layer = 8;
			}
		}

		if (connected) {
			for (int i = 0; i < rows * columns; i++) {
				int column = i % columns;
				int row = (i - column) / columns;
				var self = tiles [i].GetComponent<Tile> ();
				Debug.Log (row + " " + column);
				/*right*/
				if (column + 1 < columns) {
					var idx_neighbour = (row * columns) + (column + 1);
					Tile neighbour = tiles [idx_neighbour].GetComponent<Tile>();
					self.right = neighbour;
				}
				/*up*/
				if (row + 1 < rows) {
					var idx_neighbour = ((row + 1) * columns) + column;
					Tile neighbour = tiles [idx_neighbour].GetComponent<Tile>();
					self.up = neighbour;
				}
				/*left*/
				if (column - 1 >= 0) {
					var idx_neighbour = (row * columns) + (column - 1);
					Tile neighbour = tiles [idx_neighbour].GetComponent<Tile>();
					self.left = neighbour;
				}
				/*down*/
				if (row - 1 >= 0) {
					var idx_neighbour = ((row - 1) * columns) + column;
					Tile neighbour = tiles [idx_neighbour].GetComponent<Tile>();
					self.down = neighbour;
				}
			}
		}

	}
}