using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Y_FocalPoint : MonoBehaviour {

    public Transform FocalPoint;
    public bool clockwise = true;
    public float speed = 30f;

    // Update is called once per frame
    void Update()
    {
        int direction = clockwise ? 1 : -1;
        transform.RotateAround(FocalPoint.position, Vector3.up, direction * speed * Time.deltaTime);
    }
}
