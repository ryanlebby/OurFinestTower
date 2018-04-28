using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float range;
    public List<Unit> UnitsInRange { get; set; }

    public void Start()
    {
        UnitsInRange = new List<Unit>();
        var collider = this.GetComponent<SphereCollider>();
        collider.radius = range / 2f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitsInRange.Add(other.GetComponent<Unit>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitsInRange.Remove(other.GetComponent<Unit>());
        }
    }

    public void RefreshUnitsInRange()
    {
        foreach (var unit in UnitsInRange)
        {
            if (unit == null)
                UnitsInRange.Remove(unit);
        }
    }
}
