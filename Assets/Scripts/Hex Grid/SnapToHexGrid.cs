using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToHexGrid : MonoBehaviour {

    public HexGrid grid;

    [HideInInspector]
    public HexSlot hexSlot;

    private Vector3 previousPosition;

    private void Start()
    {
        grid.AddChild(this.transform);
        previousPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (this.transform.position.x != previousPosition.x || this.transform.position.z != previousPosition.z)
        {
            var nearestHexSlot = grid.NearestHexSlot(this.transform.position);
            this.transform.position = new Vector3(nearestHexSlot.Position.x, transform.position.y, nearestHexSlot.Position.z);
            nearestHexSlot.Contents.Add(this.gameObject);
            hexSlot = nearestHexSlot;

            previousPosition = this.transform.position;
        }
    }
}
