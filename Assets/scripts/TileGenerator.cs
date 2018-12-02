using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

    // n*m grid of tiles
    public int n;
    public int m;

    public float separationMultiplier;

    public GameObject tilePrefab;


	void Start ()
    {
        for (int z = 0; z < m; z++)
        {
            for (int x = 0; x < n; x++)
            {
                GameObject tile = GameObject.Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x * separationMultiplier, 0, z * separationMultiplier);
                tile.layer = 8;
            }
        }

	}
}
