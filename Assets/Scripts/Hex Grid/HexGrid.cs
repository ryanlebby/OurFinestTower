using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class HexGrid : MonoBehaviour {

    [Range(1,50)]
    public int width;
    [Range(1, 50)]
    public int length;
    public float yOffset;
    [Range(1,4)]
    public float outerTileRadius;
    public bool showGrid;

    private List<Vector3> snapPoints;
    private List<HexSlot> slots;
    private float innerRadius;
    private Vector3 offsetNorth;
    private Vector3 offsetNortheast;
    private Vector3 offsetSoutheast;
    private Vector3 offsetSouth;
    private Vector3 offsetSouthwest;
    private Vector3 offsetNorthwest;

    private Transform tileFolder;
    private Transform decorationFolder;
    private Transform towerFolder;
    private Transform otherFolder;

	// Use this for initialization
	void Start () {
        tileFolder = transform.Find("Tiles");
        decorationFolder = transform.Find("Decorations");
        towerFolder = transform.Find("Towers");
        otherFolder = transform.Find("Other");

        InitializeGrid();
        DrawGrid();        
	}

    void Update()
    {
        transform.position = Vector3.zero;
    }

    void OnDrawGizmos()
    {
        if (slots == null)
            slots = new List<HexSlot>();
        DrawGrid();
    }

    private void SetOffsets()
    {
        innerRadius = Mathf.Cos(30 * Mathf.Deg2Rad) * outerTileRadius;
        float distance = innerRadius * 2;
        float xOffset = Mathf.Cos(30 * Mathf.Deg2Rad) * distance;
        float zOffset = Mathf.Sin(30 * Mathf.Deg2Rad) * distance;

        // N
        offsetNorth = new Vector3(0, 0, distance);

        // S
        offsetSouth = new Vector3(0, 0, -distance);

        // SE
        offsetSoutheast = new Vector3(xOffset, 0, -zOffset);

        // SW
        offsetSouthwest = new Vector3(-xOffset, 0, -zOffset);

        // NE
        offsetNortheast = new Vector3(xOffset, 0, zOffset);

        // NW
        offsetNorthwest = new Vector3(-xOffset, 0, zOffset);
    }
    private void InitializeGrid()
    {
        OrganizeMapObjects();
        slots = new List<HexSlot>();
        SetOffsets();

        var newSlot = new HexSlot(transform.position);
        AddHexSlot(newSlot);
        Expand(newSlot);      
    }    
    private bool AddHexSlot(HexSlot newSlot)
    {
        if (!slots.Any(s => s.Position == newSlot.Position)
            && Mathf.Abs(newSlot.Position.x - transform.position.x) <= length
            && Mathf.Abs(newSlot.Position.z - transform.position.z) <= width)
        {
            slots.Add(newSlot);
            return true;
        }

        return false;
    }    
    private void Expand(HexSlot center)
    {
        if (center.N == null)
        {
            var northSlot = new HexSlot(center.Position + offsetNorth);
            if (AddHexSlot(northSlot))
            {
                center.N = northSlot;
                northSlot.S = center;
                Expand(northSlot);
            }
        }           
        
        if (center.S == null)
        {
            var southSlot = new HexSlot(center.Position + offsetSouth);
            if (AddHexSlot(southSlot))
            {
                center.S = southSlot;
                southSlot.N = center;
                Expand(southSlot);
            }
        }
        
        if (center.SE == null)
        {
            var southeastSlot = new HexSlot(center.Position + offsetSoutheast);
            if (AddHexSlot(southeastSlot))
            {
                center.SE = southeastSlot;
                southeastSlot.NW = center;
                Expand(southeastSlot);
            }
        }
        
        if (center.SW == null)
        {
            var southwestSlot = new HexSlot(center.Position + offsetSouthwest);
            if (AddHexSlot(southwestSlot))
            {
                center.SW = southwestSlot;
                southwestSlot.NE = center;
                Expand(southwestSlot);
            }
        }
          
        if (center.NE == null)
        {
            var northeastSlot = new HexSlot(center.Position + offsetNortheast);
            if (AddHexSlot(northeastSlot))
            {
                center.NE = northeastSlot;
                northeastSlot.SW = center;
                Expand(northeastSlot);
            }
        }
        
        if (center.NW == null)
        {
            var northwestSlot = new HexSlot(center.Position + offsetNorthwest);
            if (AddHexSlot(northwestSlot))
            {
                center.NW = northwestSlot;
                northwestSlot.SE = center;
                Expand(northwestSlot);
            }
        }
        
    }
    private void DrawGrid()
    {
        if (!showGrid)
        {
            return;
        }

        foreach (var slot in slots)
        {
            var vert_E = slot.Position + new Vector3(outerTileRadius, 0, 0) + transform.position;
            var vert_W = slot.Position + new Vector3(-outerTileRadius, 0, 0) + transform.position;

            float xOffset = Mathf.Cos(60 * Mathf.Deg2Rad) * outerTileRadius;
            float zOffset = Mathf.Sin(60 * Mathf.Deg2Rad) * outerTileRadius;

            var vert_NE = slot.Position + new Vector3(xOffset, 0, zOffset) + transform.position;
            var vert_NW = slot.Position + new Vector3(-xOffset, 0, zOffset) + transform.position;

            var vert_SE = slot.Position + new Vector3(xOffset, 0, -zOffset) + transform.position;
            var vert_SW = slot.Position + new Vector3(-xOffset, 0, -zOffset) + transform.position;

            Gizmos.color = new Color(200, 0, 200, 128);
            Gizmos.DrawLine(vert_E, vert_NE);
            Gizmos.DrawLine(vert_NE, vert_NW);
            Gizmos.DrawLine(vert_NW, vert_W);
            Gizmos.DrawLine(vert_W, vert_SW);
            Gizmos.DrawLine(vert_SW, vert_SE);
            Gizmos.DrawLine(vert_SE, vert_E);
        }
    }

    public void ResetGrid()
    {
        InitializeGrid();
        DrawGrid();
    }
    public HexSlot NearestHexSlot(Vector3 testPoint)
    {
        return slots
            .OrderBy(s => Vector3.Distance(new Vector3(testPoint.x, transform.position.y, testPoint.z), s.Position + transform.position))
            .FirstOrDefault();
    }
    public void AddChild(Transform child)
    {
        switch (child.tag)
        {
            case "Tile":
                child.parent = tileFolder;
                break;
            case "Decoration":
                child.parent = decorationFolder;
                break;
            case "Tower":
                child.parent = towerFolder;
                break;
            default:
                child.parent = otherFolder;
                break;
        }
    }
    public void OrganizeMapObjects()
    {
        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tile in tiles)
        {
            tile.transform.parent = tileFolder;
        }

        var decorations = GameObject.FindGameObjectsWithTag("Decoration");
        foreach (var decoration in decorations)
        {
            decoration.transform.parent = decorationFolder;
        }

        var towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (var tower in towers)
        {
            tower.transform.parent = towerFolder;
        }
    }
}
