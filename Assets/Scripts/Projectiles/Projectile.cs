using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float AttackPower;
    public float Velocity;
    public float Range;

    public bool IsFired { get; set; }

    public Transform TargetedUnit { get; set; }
    public Vector3 ProjectileTarget
    {
        get
        {
            return TargetedUnit.gameObject.GetComponent<Unit>().ProjectileTarget.position;
        }
    }
    public Transform Origin { get; set; }

    public void Start()
    {
        TargetedUnit = null;
        Origin = null;
        IsFired = false;
    }

    // If Target is no longer active or is out 
    // of range, Target = null.
    public void ValidateTarget()
    {
        if (TargetedUnit != null)
        {
            if (!TargetedUnit.gameObject.activeSelf || Vector3.Distance(Origin.position, ProjectileTarget) >= Range)
            {
                TargetedUnit = null;
            }
        }
    }

    public void MoveTowardTarget()
    {

        transform.position = Vector3.MoveTowards(
                    transform.position,
                    ProjectileTarget,
                    Velocity * Time.deltaTime
                );
    }

    public void Fire(Transform target, Transform origin)
    {
        TargetedUnit = target;
        Origin = origin;
        IsFired = true;
    }

    public virtual void Reset()
    {
        IsFired = false;
        TargetedUnit = null;
        Origin = null;
    }

    public virtual void Initialize(Tower tower)
    {
        tower.LoadedProjectile = this;
        AttackPower += tower.AttackPower;
        Range = tower.Range;
    }
}
