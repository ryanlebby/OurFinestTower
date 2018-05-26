using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float AttackPower;
    public float Velocity;
    public float Range;

    public bool IsFired { get; set; }

    public Unit TargetedUnit { get; set; }
    public Transform Origin { get; set; }

    protected float tempVelocity;

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
            if (!TargetedUnit.gameObject.activeSelf || Vector3.Distance(Origin.position, TargetedUnit.ProjectileContactPoint.position) >= Range)
            {
                TargetedUnit = null;
            }
        }
    }

    public void MoveTowardTarget()
    {
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    TargetedUnit.ProjectileContactPoint.position,
                    Velocity * Time.deltaTime
                );
    }

    public void MoveTowardTargetSlowToFast(float velocityMultiplier)
    {
        tempVelocity *= velocityMultiplier;
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    TargetedUnit.ProjectileContactPoint.position,
                    tempVelocity * Time.deltaTime
                );
    }

    public virtual void Fire(Unit target, Transform origin)
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
