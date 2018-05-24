using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystic : Tower
{
    void Update()
    {   
        // Load Projectile
        if (LoadedProjectile == null)
        {
            LoadProjectile();
        }

        // If tower is not on cooldown.
        if (!OnCooldown)
        {
            ValidateTarget();
            // If no target, search for one.
            if (TargetedEnemy == null)
            {
                AcquireTarget();
            }                

            // If target exists, fire.
            if (TargetedEnemy != null)
            {
                LoadedProjectile.Fire(TargetedEnemy, ProjectileSpawnPoint);
                LoadedProjectile = null;
                StartCoroutine("WaitForAttackCooldown");
            }
        }
    }
}
