using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public float waveHeight;
    public float waveSpeed;
    //public float waveRotation;

    private float yOffset;

    private float minHeight;
    private float maxHeight;
    private bool waveHeightUp;
    private Vector3 targetHeight;

    private float minRotX;
    private float maxRotX;
    private float targetRotationX;
    private bool waveRotationXUp;

    private float minRotZ;
    private float maxRotZ;    
    private float targetRotationZ; 
    private bool waveRotationZUp;


    // Use this for initialization
    void Start () {
        yOffset = transform.position.y;

        minHeight = yOffset - waveHeight;
        maxHeight = yOffset + waveHeight;
        targetHeight = new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), transform.position.z);
        waveHeightUp = (targetHeight.y > transform.position.y);

        //minRotX = transform.rotation.x - waveRotation;
        //maxRotX = transform.rotation.x - waveRotation;
        //targetRotationX = Random.Range(minRotX, maxRotX);
        //waveRotationXUp = (targetRotationX > transform.rotation.x);        
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpAndDown();
        //Rotation();
    }

    void UpAndDown()
    {
        if (waveHeightUp && transform.position.y >= targetHeight.y)
        {
            waveHeightUp = !waveHeightUp;
            targetHeight = new Vector3(transform.position.x, Random.Range(minHeight, transform.position.y), transform.position.z);
        }

        if (!waveHeightUp && transform.position.y <= targetHeight.y)
        {
            waveHeightUp = !waveHeightUp;
            targetHeight = new Vector3(transform.position.x, Random.Range(transform.position.y, maxHeight), transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetHeight, waveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        
        if (waveRotationXUp && transform.rotation.x <= maxRotX)
        {
            waveRotationXUp = !waveRotationXUp;
            targetRotationX = Random.Range(minRotX, targetRotationX);
        }

        if (!waveRotationXUp && transform.rotation.x >= minRotX)
        {
            waveRotationXUp = !waveRotationXUp;
            targetRotationX = Random.Range(targetRotationX, maxRotX);
        }

        transform.Rotate(Vector3.right, (transform.rotation.x - targetRotationX) * Time.deltaTime);
    }

}
