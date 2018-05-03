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
            if (Target == null)
            {
                AcquireTarget();
            }                

            // If target exists, fire.
            if (Target != null)
            {
                LoadedProjectile.Fire(Target, ProjectileSpawnPoint);
                LoadedProjectile = null;
                StartCoroutine("WaitForAttackCooldown");
            }
        }
    }
}
