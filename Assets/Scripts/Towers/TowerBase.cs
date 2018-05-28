using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour {

    public Transform SpawnPointTransform;
    public Transform Platform;

    [HideInInspector]
    public bool HasTower;

    public Vector3 Position { get { return SpawnPointTransform.position; } }

    public void Start()
    {
        HasTower = false;
        float angle = Random.Range(0f,359f);
        Platform.Rotate(Vector3.up, angle);
    }
}
