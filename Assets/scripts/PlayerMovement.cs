using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	void Start ()
    {

	}
	

	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            tileRaycast();
        }
    }

    void tileRaycast()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        // This casts rays only against colliders in layer 8.
        int layerMask = 1 << 8;

        Ray ray = Camera.main.ViewportPointToRay(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log("Tile at: " + hit.transform.position);

            movePlayer(hit.transform.position);
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    void movePlayer(Vector3 newPosition)
    {

        newPosition.y = transform.position.y;
        transform.position = newPosition;

        

    }

}
