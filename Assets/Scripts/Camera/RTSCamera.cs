using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

    public float panSpeed = 20f;
    public float scrollSpeed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            var h = panSpeed * Time.deltaTime;
            var sides = Mathf.Sqrt(Mathf.Pow(h, 2) / 2);
            pos += new Vector3(-sides, 0, sides);
        }

        if (Input.GetKey("d"))
        {
            var h = panSpeed * Time.deltaTime;
            var sides = Mathf.Sqrt(Mathf.Pow(h, 2) / 2);
            pos += new Vector3(sides, 0, sides);
        }

        if (Input.GetKey("s"))
        {
            var h = panSpeed * Time.deltaTime;
            var sides = Mathf.Sqrt(Mathf.Pow(h, 2) / 2);
            pos += new Vector3(sides, 0, -sides);
        }

        if (Input.GetKey("a"))
        {            
            var h = panSpeed * Time.deltaTime;
            var sides = Mathf.Sqrt(Mathf.Pow(h, 2) / 2);
            pos += new Vector3(-sides, 0, -sides);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            var newPos = new Vector3(0, scroll * scrollSpeed * 100 * Time.deltaTime, 0);
            pos -= newPos;
        }            

        if (transform.position != pos)
            transform.position = pos;
    }
}
