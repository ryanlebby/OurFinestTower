using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{    
    public float SelfDestructTimer;
    public bool SelfDestructMode { get; set; }

    new void Start()
    {
        base.Start();
        SelfDestructMode = false;
    }

    void Update () {

        //if (!this.gameObject.activeSelf)
        //{
        //    SelfDestructMode = false;
        //}            

        if (IsFired && !SelfDestructMode)
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

    IEnumerator SelfDestruct()
    {
        SelfDestructMode = true;
        float timer = SelfDestructTimer;

        while (timer > 0f)
        {
            ValidateTarget();

            if (Target == null)
                break;

            timer -= Time.deltaTime;
            yield return null;
        }

        SelfDestructMode = false;
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        SelfDestructMode = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == Target)
        {
            Target.GetComponent<Unit>().TakeDamage(AttackPower);            

            if (Target.gameObject.activeSelf)
                this.transform.parent = Target;

            StartCoroutine("SelfDestruct");
        }
    }

    new void Reset()
    {
        base.Reset();
        SelfDestructMode = false;
    }
}
