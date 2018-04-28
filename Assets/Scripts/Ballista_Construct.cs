using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista_Construct : MonoBehaviour {

    private Transform tower;
    private Transform joint;
    private Transform stand;

	void Start () {
        tower = transform.Find("tower");
        joint = transform.Find("joint");
        stand = transform.Find("stand");

        //tower.transform.position = joint.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        var euler = tower.rotation.eulerAngles;   //get target's rotation
        var rot = Quaternion.Euler(stand.rotation.x, stand.rotation.y, euler.z); //transpose values
        //stand.rotation = rot;                  //set my rotation


    }
}


