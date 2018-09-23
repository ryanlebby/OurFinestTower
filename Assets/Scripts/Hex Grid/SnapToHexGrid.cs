using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToHexGrid : MonoBehaviour {

    public HexGrid grid;
    public float yOffset;

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
            this.transform.position = new Vector3(this.transform.position.x, grid.transform.position.y + yOffset, this.transform.position.z);

            var nearestHexSlot = grid.NearestHexSlot(this.transform.position);
            this.transform.position = nearestHexSlot.Position + Vector3.up * yOffset;
            nearestHexSlot.Contents.Add(this.gameObject);

            previousPosition = this.transform.position;
        }
    }
}
