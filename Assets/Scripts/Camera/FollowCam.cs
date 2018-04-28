using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform unit;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(unit.position.x - 10f, 20f, unit.position.z - 10f);
        transform.LookAt(unit);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(unit.position.x - 10f, 20f, unit.position.z - 10f);
	}
}
