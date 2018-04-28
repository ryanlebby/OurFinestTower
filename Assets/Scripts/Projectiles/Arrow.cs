using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{

    public float BaseAttackPower = 0f;
    public float BaseSpeed = 10f;
    public Unit Target = null;

    public float AttackDamage { get; set; }
    public Transform Tower { get; set; }
    public float Range { get; set; }
    public bool IsFired { get; set; }

    private bool selfDestructMode = false;
    private float selfDestructTimer = 0f;
    private float selfDestructLimit = 0.8f;

    // Use this for initialization
    void Start () {
        IsFired = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (selfDestructMode)
        {
            if (Target == null || !Target.gameObject.activeSelf)
            {
                Destroy(this.gameObject);
            }

            selfDestructTimer += Time.deltaTime;
            if (selfDestructTimer >= selfDestructLimit)
                Destroy(this.gameObject);
        }

        else if (IsFired)
        {
            if (Vector3.Distance(transform.position, Tower.transform.position) > Range || !Target.gameObject.activeSelf)
            {
                Destroy(this.gameObject);
            }

            else
            {
                transform.LookAt(Target.transform);

                if (transform.position != Target.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, BaseSpeed * Time.deltaTime);
                }
            }
        }        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() == Target)
        {
            Target.TakeDamage(AttackDamage);
            selfDestructMode = true;

            if (Target.gameObject.activeSelf)
            {
                this.transform.parent = Target.transform;
            }
        }
    }
}
