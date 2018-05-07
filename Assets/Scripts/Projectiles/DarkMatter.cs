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
    private float timer;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        initialSize = transform.localScale;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpawning)
        {
            if (transform.localScale.x <= initialSize.x)
            {
                transform.localScale *= 1 + SpawnGrowthSpeed;
            }

            else
            {
                transform.localScale = initialSize;
                IsSpawning = false;
            }            
        }

        else if (IsDetonating)
        {
            if (timer > 0)
            {
                timer -= DetonateTimer * Time.deltaTime;
                transform.localScale += Vector3.one * DetonateGrowthSpeed;
            }
            
            else
            {
                foreach (var unit in GameManager.Instance.ActiveUnits)
                {
                    if (Vector3.Distance(transform.position, unit.transform.position) <= DetonateRange)
                    {
                        unit.TakeDamage(AttackPower * DetonateDmgMultiplier);
                    }
                }

                IsDetonating = false;
                transform.localScale = initialSize;
                gameObject.SetActive(false);
            }
        }

        // Fired, but not detonated
        else if (IsFired)
        {
            ValidateTarget();

            // If no target
            if (Target == null)
            {
                gameObject.SetActive(false);
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
        IsDetonating = false;
        transform.localScale = initialSize;
        Spawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == Target && !IsDetonating)
        {
            Target.GetComponent<Unit>().TakeDamage(AttackPower);
            Target = null;
            IsFired = false;
            Detonate();
        }       
    }

    void Detonate()
    {
        IsDetonating = true;
        timer = DetonateTimer;
    }

    void Spawn()
    {
        IsSpawning = true;
        transform.localScale = initialSize / 10f;
    }
}
