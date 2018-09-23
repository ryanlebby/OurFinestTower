using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public float waveSize;
    public float waveSpeed;
    private float yOffset;
    private float min;
    private float max;
    private Vector3 targetPosition;
    private bool waveDirectionUp;


	// Use this for initialization
	void Start () {
        yOffset = transform.position.y;
        min = yOffset - waveSize;
        max = yOffset + waveSize;
        targetPosition = new Vector3(transform.position.x, Random.Range(min, max), transform.position.z);
        waveDirectionUp = (targetPosition.y > transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {

        if (waveDirectionUp && transform.position.y >= targetPosition.y)
        {
            waveDirectionUp = !waveDirectionUp;
            targetPosition = new Vector3(transform.position.x, Random.Range(min, transform.position.y), transform.position.z);
        }

        if (!waveDirectionUp && transform.position.y <= targetPosition.y)
        {
            waveDirectionUp = !waveDirectionUp;
            targetPosition = new Vector3(transform.position.x, Random.Range(transform.position.y, max), transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, waveSpeed * Time.deltaTime);
    }
}
