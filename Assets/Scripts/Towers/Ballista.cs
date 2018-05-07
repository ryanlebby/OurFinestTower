using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : Tower
{
    private Animator anim;

    new void Start()
    {
        base.Start();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        ValidateTarget();

        if (LoadedProjectile == null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            LoadProjectile();
        }

        if (Target != null)
        {
            transform.LookAt(Target.transform);
            if (LoadedProjectile != null)
                LoadedProjectile.transform.rotation = transform.rotation;
        }

        if (!OnCooldown)
        {
            if (Target == null)
                AcquireTarget();

            if (Target != null)
            {
                anim.Play("Fire");
                LoadedProjectile.Fire(Target, ProjectileSpawnPoint);
                LoadedProjectile.Origin = this.transform;
                LoadedProjectile = null;
                StartCoroutine("WaitForAttackCooldown");
            }
        }
    }
}
