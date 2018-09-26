using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexSlot
{
    public Vector3 Position { get; set; }
    public List<GameObject> Contents { get; set; }
    public List<string> ContentTypes { get; set; }

    // Neighbors
    public HexSlot N { get; set; }
    public HexSlot NE { get; set; }
    public HexSlot NW { get; set; }
    public HexSlot SW { get; set; }
    public HexSlot SE { get; set; }
    public HexSlot S { get; set; }

    public HexSlot(Vector3 position)
    {
        Position = position;
        Contents = new List<GameObject>();
    }

    public HexSlot(Vector3 position, GameObject gameObject)
    {
        Position = position;
        Contents.Add(gameObject);
    }
}
