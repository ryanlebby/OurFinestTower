using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapTileDisplay : MonoBehaviour {

    public MapTile mapTile;
    public MeshRenderer mesh;
    
    void Start()
    {
        mesh.material = mapTile.material;
    }

    void OnValidate () {
        mesh.material = mapTile.material;
	}
}
