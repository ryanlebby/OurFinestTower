using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DarkMatter : Projectile
{
    public float SpawnGrowthSpeed = 0.075f;    
    public float DetonateTimer = 2.0f;
    public float DetonateGrowthSpeed = 0.075f;
    public float DetonateRange = 3f;
    public float DetonateDmgMultiplier = 1.5f;
    public bool IsDetonating { get; set; }
    public bool IsSpawning { get; set; }

    private Vector3 initialSize;
    private Vector3 spawnSize;    
    private float timer;
    private bool flagAsInactive = false;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        initialSize = transform.localScale;
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (flagAsInactive)
        {
            this.gameObject.SetActive(false);
            Reset();
            flagAsInactive = false;
        }

        // Fired, but not detonated
        if (IsFired && !IsDetonating && !IsSpawning)
        {
            ValidateTarget();

            // If no target
            if (Target == null)
            {
                flagAsInactive = true;
            }

            // If projectile has not reached target,
            // keep moving towards the target
            else if (transform.position != Target.transform.position)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    Target.transform.position, 
                    Velocity * Time.deltaTime
                );
            }
        }       
    }

    public override void Reset()
    {
        base.Reset();
        IsSpawning = false;
        IsDetonating = false;
        transform.localScale = initialSize;
    }

    void OnTriggerEnter(Collider other)
    {
        //if (Target != null)
        //{
            if (other.transform == Target && !IsDetonating)
            {
                Target.GetComponent<Unit>().TakeDamage(AttackPower);
                Target = null;
                IsFired = false;
                StartCoroutine("Detonate");
            }
        //}        
    }

    IEnumerator Detonate()
    {
        IsDetonating = true;
        timer = DetonateTimer;

        while (timer > 0)
        {
            timer -= DetonateTimer * Time.deltaTime;
            transform.localScale += Vector3.one * DetonateGrowthSpeed;
            yield return null;
        }

        foreach (var unit in GameManager.Instance.ActiveUnits)
        {
            if (Vector3.Distance(transform.position, unit.transform.position) <= DetonateRange)
            {
                unit.TakeDamage(AttackPower * DetonateDmgMultiplier);
            }
        }

        IsDetonating = false;
        transform.localScale = initialSize;
        flagAsInactive = true;        
    }

    IEnumerator Spawn()
    {
        IsSpawning = true;
        transform.localScale = initialSize / 10f;

        while (transform.localScale.x <= initialSize.x)
        {
            transform.localScale *= 1 + SpawnGrowthSpeed;
            yield return null;
        }

        transform.localScale = initialSize;
        IsSpawning = false;
    }
}
