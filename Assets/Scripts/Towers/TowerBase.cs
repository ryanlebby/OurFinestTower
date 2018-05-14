using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour {

    public Transform SpawnPointTransform;

    [HideInInspector]
    public bool HasTower = false;

    public Vector3 Position { get { return SpawnPointTransform.position; } }
}
