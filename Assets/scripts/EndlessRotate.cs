using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRotate : MonoBehaviour {

    [SerializeField]
    float speed;

    Transform sprite;
    Vector3 rotation = new Vector3(0.0f,1.0f,0.0f);

	// Use this for initialization
	void Start ()
    {
        sprite = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        sprite.Rotate(speed * rotation * Time.deltaTime);
	}
}
