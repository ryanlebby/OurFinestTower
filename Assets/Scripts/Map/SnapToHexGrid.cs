using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToHexGrid : MonoBehaviour {

    public HexGrid grid;
    //public float snapDistance;
    public float yOffset;

    private Vector3 previousPosition;

    private void Start()
    {
        this.transform.parent = grid.transform;
        previousPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (this.transform.position != previousPosition)
        {
            this.transform.position = new Vector3(this.transform.position.x, grid.transform.position.y + yOffset, this.transform.position.z);

            //var nearestSnapPoint = grid.NearestSnapPoint(transform.position);
            var nearestHexSlot = grid.NearestHexSlot(this.transform.position);
            //if (Vector3.Distance(transform.position, nearestSnapPoint) <= snapDistance)
            this.transform.position = nearestHexSlot.Position + Vector3.up * yOffset;
            nearestHexSlot.Content = this.gameObject;

            previousPosition = this.transform.position;
        }
    }
}
