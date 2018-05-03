using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float AttackPower;
    public float Velocity;
    public float Range;

    public bool IsFired { get; set; }

    public Transform Target { get; set; }
    public Transform Origin { get; set; }

    public void Start()
    {
        Target = null;
        Origin = null;
        IsFired = false;
    }

    // If Target is no longer active or is out 
    // of range, Target = null.
    public void ValidateTarget()
    {
        if (Target != null)
        {
            if (!Target.gameObject.activeSelf || Vector3.Distance(Origin.position, Target.position) >= Range)
            {
                Target = null;
            }
        }
    }

    public void Fire(Transform target, Transform origin)
    {
        Target = target;
        Origin = origin;
        IsFired = true;
    }

    public virtual void Reset()
    {
        IsFired = false;
        Target = null;
        Origin = null;
    }

    public virtual void Initialize(Tower tower)
    {
        tower.LoadedProjectile = this;
        AttackPower += tower.AttackPower;
        Range = tower.Range;
    }
}
