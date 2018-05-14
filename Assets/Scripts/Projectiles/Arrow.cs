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

    void Update () {

        if (SelfDestructMode)
        {            
            if (timer > 0)
            {
                ValidateTarget();

                if (Target == null)
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

            if (Target == null)
            {
                IsFired = false;
                this.gameObject.SetActive(false);
            }

            else
            {
                transform.LookAt(Target.transform);

                if (transform.position != Target.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Velocity * Time.deltaTime);
                }
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
        if (other.transform == Target)
        {
            Target.GetComponent<Unit>().TakeDamage(AttackPower);
            SelfDestruct();

            if (Target.gameObject.activeSelf)
            {
                transform.parent = Target;
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
