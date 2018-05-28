using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Y : MonoBehaviour {

    public bool X, Y, Z;

    public bool clockwise = true;
    public float speed = 30f;
	
	// Update is called once per frame
    
	void Update () {

        int direction = clockwise ? 1 : -1;
        transform.Rotate(
            X ? direction * speed * Time.deltaTime : 0,
            Y ? direction * speed * Time.deltaTime : 0, 
            Z ? direction * speed * Time.deltaTime : 0
        );
    }
}
