using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomHexOrientation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int orientation = Random.Range(0, 6) * 60;
        transform.rotation = Quaternion.AngleAxis(orientation, Vector3.up);
    }
}
