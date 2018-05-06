using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Y : MonoBehaviour {

    public bool clockwise = true;
    public float speed = 30f;
	
	// Update is called once per frame
	void Update () {
        int direction = clockwise ? 1 : -1;
        transform.Rotate(0, 0, direction * speed * Time.deltaTime);
    }
}
