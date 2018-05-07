using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStand : MonoBehaviour {

    public GameObject Rotator;
    private Transform Tower;

	// Use this for initialization
	void Start () {
        Tower = transform.parent.GetComponentInChildren<Tower>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        var euler = Tower.rotation.eulerAngles;
        Rotator.transform.rotation = Quaternion.Euler(0, euler.y, 0);
	}
}
