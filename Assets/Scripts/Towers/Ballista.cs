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

    private ObjectPool ArrowPool = new ObjectPool();
    private Arrow LoadedProjectile = null;
    private Unit target = null;
    private float RemainingCooldown = 0;
    private Animator anim;

    // Use this for initialization
    new void Start()
    {
        BaseRange = Range;
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

        if (target != null && target.gameObject.activeSelf)
        {
            transform.LookAt(target.transform);
            if (LoadedProjectile != null)
                LoadedProjectile.transform.rotation = transform.rotation;
        }

        if (RemainingCooldown <= 0)
        {
            /// MIGHT BE THE SLOWDOWN ISSUE!!!! CALLING EVERY FRAME?
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
        GameObject go = ArrowPool.GetNextAvailable();

        if (go != null)
        {
            go.transform.position = ProjectileSpawn.position;
            go.transform.rotation = transform.rotation;
            go.GetComponent<Arrow>().Reset();
        }
        
        else
        {
            go = Instantiate(projectilePrefab, ProjectileSpawn.position, transform.rotation);
            go.transform.parent = transform;
        }
        
        var arrow = go.GetComponent<Arrow>();
        arrow.AttackDamage = arrow.BaseAttackPower + TowerAttackPower;
        arrow.Tower = transform;
        arrow.Range = Range;
        LoadedProjectile = arrow;
    }

    void Fire()
    {
        anim.Play("Fire");
        LoadedProjectile.Target = target;
        LoadedProjectile.IsFired = true;
        LoadedProjectile = null;
    }
}
