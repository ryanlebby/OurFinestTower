using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPath : MonoBehaviour {

    public Transform[] Waypoints;
    public PlayerBase PlayerBase;

    public int Length { get { return Waypoints.Length; } }

    public Transform At(int index)
    {
        if (index < Waypoints.Length)
            return Waypoints[index];

        return null;
    }
}
