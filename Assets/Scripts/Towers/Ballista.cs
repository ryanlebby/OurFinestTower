using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : Tower
{

    public float TowerAttackPower = 2f;
    public float Range = 12f;
    public float TimeBetweenAttacks = 1.5f;
    public GameObject projectilePrefab;
    public Transform ProjectileSpawn;


    private float RemainingCooldown = 0;
    private Unit target = null;
    private Arrow LoadedProjectile = null;

    private Animator anim;

    // Use this for initialization
    new void Start()
    {
        range = Range;
        base.Start();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (LoadedProjectile == null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            LoadProjectile();
        }

        if (target != null)
        {
            transform.LookAt(target.transform);
            if (LoadedProjectile != null)
                LoadedProjectile.transform.rotation = transform.rotation;
        }

        if (RemainingCooldown <= 0)
        {
            if (target == null || Vector3.Distance(transform.position, target.transform.position) >= Range)
                SearchForTarget();

            if (target != null)
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

        foreach (var curUnit in GameManager.Instance.ActiveUnits)
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
        GameObject go = Instantiate(projectilePrefab, ProjectileSpawn.position, transform.rotation);
        go.transform.parent = transform;
        var p = go.GetComponent<Arrow>();
        p.AttackDamage = p.BaseAttackPower + TowerAttackPower;
        p.Tower = transform;
        p.Range = Range;
        LoadedProjectile = p;
    }

    void Fire()
    {
        anim.Play("Fire");
        LoadedProjectile.Target = target;
        LoadedProjectile.IsFired = true;
        LoadedProjectile = null;
    }
}
