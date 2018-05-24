using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{    
    public float SelfDestructDuration;
    public bool SelfDestructMode { get; set; }

    private float timer;
    private Vector3 originalScale;
    private Vector3 scaleDuringSelfDestruct;

    new void Start()
    {
        base.Start();
        SelfDestructMode = false;
        originalScale = transform.localScale;
    }

    void FixedUpdate () {

        if (SelfDestructMode)
        {            
            if (timer > 0)
            {
                ValidateTarget();

                if (TargetedUnit == null)
                    timer = 0;
                else
                    timer -= Time.deltaTime;

                transform.localScale = scaleDuringSelfDestruct;
            }

            else
            {
                SelfDestructMode = false;
                this.transform.parent = Origin.transform;
                transform.localScale = originalScale;
                this.gameObject.SetActive(false);
            }

        }           

        if (IsFired)
        {
            ValidateTarget();

            if (TargetedUnit == null)
            {
                IsFired = false;
                this.gameObject.SetActive(false);
            }

            else
            {
                transform.LookAt(ProjectileTarget);
                MoveTowardTarget();
            }
        }        
	}

    void SelfDestruct()
    {
        SelfDestructMode = true;
        IsFired = false;
        timer = SelfDestructDuration;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == TargetedUnit)
        {
            TargetedUnit.GetComponent<Unit>().TakeDamage(AttackPower);
            SelfDestruct();

            if (TargetedUnit.gameObject.activeSelf)
            {
                transform.parent = TargetedUnit;
                scaleDuringSelfDestruct = transform.localScale;
            }                            
        }
    }

    new void Reset()
    {
        base.Reset();
        SelfDestructMode = false;
        transform.localScale = originalScale;
    }
}
