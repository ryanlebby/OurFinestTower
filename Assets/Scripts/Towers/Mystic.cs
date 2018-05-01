using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystic : Tower
{

    public float TowerAttackPower = 8f;
    public float Range = 24f;
    public float TimeBetweenAttacks = 3f;
    public GameObject projectilePrefab;
    public Transform SpawnPoint;
    private DarkMatter LoadedProjectile = null;

    private ObjectPool DarkMatterPool = new ObjectPool();
    private Unit target = null;
    private float RemainingCooldown = 0;
    

    // Use this for initialization
    public new void Start()
    {
        BaseRange = Range;
        base.Start();        
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadedProjectile == null)
        {
            LoadProjectile();
        }

        if (RemainingCooldown <= 0)
        {
            if (target == null || !target.gameObject.activeSelf || Vector3.Distance(transform.position, target.transform.position) >= Range)
                SearchForTarget();

            if (target != null && target.gameObject.activeSelf)
            {
                Fire();
                RemainingCooldown = TimeBetweenAttacks;
            }
        }

        else
        {
            RemainingCooldown -= Time.deltaTime;
        }
    }

    void SearchForTarget()
    {
        float nearestDistance = Range + 1f;
        Unit nearestUnit = null;

        RefreshUnitsInRange();

        foreach (var curUnit in UnitsInRange)
        {
            float curDist = Vector3.Distance(transform.position, curUnit.transform.position);

            if (nearestUnit == null || curDist < nearestDistance)
            {
                nearestUnit = curUnit;
                nearestDistance = curDist;
            }           
        }

        if (nearestDistance <= Range)
        {
            target = nearestUnit;
        }
        else
        {
            target = null;
        }
    }    

    void LoadProjectile()
    {
        GameObject go = DarkMatterPool.GetNextAvailable();

        if (go != null)
        {
            go.SetActive(true);
            go.transform.position = SpawnPoint.position;
            go.transform.rotation = transform.rotation;
            go.GetComponent<DarkMatter>().Reset();
        }
        else
        {
            go = Instantiate(projectilePrefab, SpawnPoint.position, transform.rotation);
            go.transform.parent = transform;
            DarkMatterPool.Add(go);
        }
        
        var dm = go.GetComponent<DarkMatter>();
        LoadedProjectile = dm;
        dm.TowerAttackPower = TowerAttackPower;
        dm.Tower = transform;
        dm.Range = Range;        
    }

    void Fire()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            LoadedProjectile.Target = target;
            LoadedProjectile.IsFired = true;

            LoadedProjectile = null;
        }        
    }
}
