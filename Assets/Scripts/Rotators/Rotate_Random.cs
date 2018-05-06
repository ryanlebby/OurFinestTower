using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Random : MonoBehaviour {

    private Vector3 rotation = new Vector3(1, 0, 0);
    private float speed = 0f;
    private float maxSpeed = 90f;
    private float acceleration = 3f;
    private bool rampingUp = true;

    // Update is called once per frame
    void Update()
    {
        speed = rampingUp
            ? speed + acceleration
            : speed - acceleration;

        if (speed <= 0)
        {
            getNewRotation();
            getNewSpeedAndAcceleration();
            rampingUp = true;
        }

        else if (speed >= maxSpeed)
        {
            rampingUp = false;
        }

        transform.Rotate(new Vector3(
            rotation.x * speed * Time.deltaTime,
            rotation.y * speed * Time.deltaTime,
            rotation.z * speed * Time.deltaTime
            ));
    }

    void getNewRotation()
    {
        rotation = new Vector3(
            (float)Random.Range(-1, 2),
            (float)Random.Range(-1, 2),
            (float)Random.Range(-1, 2)
            );

        if (rotation == Vector3.zero)
            getNewRotation();
    }

    void getNewSpeedAndAcceleration()
    {
        speed = 0f;
        acceleration = Random.Range(5, 10);
        maxSpeed = Random.Range(180, 361);
    }
}
