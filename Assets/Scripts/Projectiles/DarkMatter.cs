using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DarkMatter : Projectile
{
    public GameObject Core;
    public float SpawnGrowthSpeed = 0.075f;    
    public float DetonateTimer = 2.0f;
    public float DetonateGrowthSpeed = 0.075f;
    public float DetonateRange = 3f;
    public float DetonateDmgMultiplier = 1.5f;
    public bool IsDetonating { get; set; }
    public bool IsSpawning { get; set; }

    private Vector3 initialSize;
    private Renderer coreRenderer;
    private Color initialCoreColor;
    private float timer;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        initialSize = transform.localScale;
        coreRenderer = Core.GetComponent<Renderer>();
        initialCoreColor = coreRenderer.material.color;
        Spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
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
                coreRenderer.material.color = Color.Lerp(
                    coreRenderer.material.color,
                    Color.red,
                    1.0f - timer / DetonateGrowthSpeed);
            }
            
            else
            {
                foreach (var unit in GameManager.Instance.ActiveUnits)
                {
                    if (Vector3.Distance(transform.position, unit.ProjectileContactPoint.position) <= DetonateRange)
                    {
                        unit.TakeDamage(AttackPower * DetonateDmgMultiplier);
                    }
                }

                IsDetonating = false;
                transform.localScale = initialSize;
                coreRenderer.material.color = initialCoreColor;
                gameObject.SetActive(false);
            }
        }

        // Fired, but not detonated
        else if (IsFired)
        {
            ValidateTarget();

            // If no target
            if (TargetedUnit == null)
            {
                gameObject.SetActive(false);
            }

            // If projectile has not reached target,
            // keep moving towards the target
            else if (transform.position != TargetedUnit.transform.position)
            {
                MoveTowardTarget();
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
        if (other.transform == TargetedUnit.transform && !IsDetonating)
        {
            TargetedUnit.TakeDamage(AttackPower);
            TargetedUnit = null;
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
