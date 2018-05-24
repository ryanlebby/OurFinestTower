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

        if (TargetedEnemy != null)
        {
            transform.LookAt(TargetedEnemy.ProjectileContactPoint);
            if (LoadedProjectile != null)
                LoadedProjectile.transform.rotation = transform.rotation;
        }

        if (!OnCooldown)
        {
            if (TargetedEnemy == null)
                AcquireTarget();

            if (TargetedEnemy != null)
            {
                anim.Play("Fire");
                LoadedProjectile.Fire(TargetedEnemy, ProjectileSpawnPoint);
                LoadedProjectile.Origin = this.transform;
                LoadedProjectile = null;
                StartCoroutine("WaitForAttackCooldown");
            }
        }
    }
}
