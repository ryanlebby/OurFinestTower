using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This will be the base class for all towers (I think).
// Some towers might work so differently that we'll need to revise.
public class Tower : MonoBehaviour {
    public string name;
    public float AttackPower;
    public float AttackSpeed;
    public float Range;
    public GameObject ProjectilePrefab;
    public Transform ProjectileSpawnPoint;

    public Transform Target { get; set; }
    public Projectile LoadedProjectile { get; set; }    
    public List<Unit> UnitsInRange { get; set; }
    public ObjectPool ProjectilePool { get; set; }
    public bool OnCooldown { get; set; }

    // START
    public void Start()
    {
        UnitsInRange = new List<Unit>();
        ProjectilePool = new ObjectPool();
        OnCooldown = false;
        Target = null;
        LoadedProjectile = null;

        var collider = this.GetComponent<SphereCollider>();
        collider.radius = Range;        
    }

    // COLLISION - ENTER
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitsInRange.Add(other.GetComponent<Unit>());
        }
    }

    // COLLISION - EXIT
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitsInRange.Remove(other.GetComponent<Unit>());
        }
    }

    // Remove inactive or null units from list.
    public void RefreshUnitsInRange()
    {
        foreach (var unit in UnitsInRange)
        {
            if (unit == null || !unit.gameObject.activeSelf)
                UnitsInRange.Remove(unit);
        }
    }

    // If Target is no longer active or is out 
    // of range, Target = null.
    public void ValidateTarget()
    {
        if (Target != null)
        {
            if (!Target.gameObject.activeSelf || Vector3.Distance(transform.position, Target.position) >= Range)
            {
                Target = null;
            }
        }        
    }

    public void AcquireTarget()
    {
        // Remove inactive or null units from UnitsInRange.
        RefreshUnitsInRange();
        
        if (UnitsInRange.Count == 0)
            Target = null;

        else
        {
            // Order by distance from tower and take the first entry
            var nearestUnit = UnitsInRange
            .OrderBy(u => Vector3.Distance(transform.position, u.transform.position))
            .FirstOrDefault();

            Target = nearestUnit.transform;
        }        
    }

    public void LoadProjectile()
    {
        Projectile projectile;
        GameObject go = ProjectilePool.GetNextAvailable();

        // Use existing projectile instance if one is available.
        if (go != null)
        {
            go.transform.position = ProjectileSpawnPoint.position;
            go.transform.rotation = transform.rotation;
            go.transform.parent = transform;
            projectile = go.GetComponent<Projectile>();            
            go.SetActive(true);
            projectile.Reset();
        }

        // If there are none available, create a new instance.
        else
        {
            go = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, transform.rotation);
            go.transform.parent = transform;            
            ProjectilePool.Add(go);
            projectile = go.GetComponent<Projectile>();
            projectile.Initialize(this);
        }

        LoadedProjectile = projectile;
    }

    // Coroutine to set cooldown = true until 
    // AttackSpeed seconds have elapsed.
    public IEnumerator WaitForAttackCooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        OnCooldown = false;
    }
}
