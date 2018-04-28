using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StatusDM
{
    Normal,
    Spawning,    
    Detonating
}

public class DarkMatter : Projectile
{
    public float BaseAttackPower = 0f;
    public float BaseSpeed = 10f;
    public Unit Target = null;
    public float DetonateRange = 3f;
    public float SizeIncrease = 0.075f;
    public float SpawnSpeed = 0.075f;

    public float AttackDamage { get; set; }
    public Transform Tower { get; set; }
    public float Range { get; set; }
    public bool IsFired { get; set; }

    private StatusDM status = StatusDM.Normal;
    private Vector3 spawnSize;
    public float detonationTimer = 2.0f;
    private float timer;


    // Use this for initialization
    void Start()
    {
        IsFired = false;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == StatusDM.Spawning)
        {
            UpdateSpawn();
        }

        else if (status == StatusDM.Detonating)
        {
            UpdateDetonation();
        }

        else
        {
            if (IsFired)
            {
                if (Vector3.Distance(transform.position, Tower.transform.position) > Range || Target == null)
                {
                    Destroy(this.gameObject);
                }

                else
                {
                    if (transform.position != Target.transform.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, BaseSpeed * Time.deltaTime);
                    }
                }
            }
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() == Target)
        {
            Target.TakeDamage(AttackDamage);
            Detonate();
        }            
    }

    void UpdateDetonation()
    {
        timer -= detonationTimer * Time.deltaTime;

        transform.localScale += Vector3.one * SizeIncrease;        

        if (timer <= 0)
        {
            foreach (var unit in GameManager.Instance.ActiveUnits)
            {
                if (Vector3.Distance(transform.position, unit.transform.position) <= DetonateRange)
                {
                    unit.TakeDamage(AttackDamage * 1.5f);
                }
            }
            Destroy(this.gameObject);            
        }
    }

    void UpdateSpawn()
    {
        transform.localScale *= 1 + SpawnSpeed;
        var col = this.gameObject.GetComponentInChildren<Renderer>().material.color;
        col.a = 0.5f;

        if (transform.localScale.x >= spawnSize.x)
        {
            transform.localScale = spawnSize;
            status = StatusDM.Normal;
        }
    }

    void Detonate()
    {
        status = StatusDM.Detonating;
        timer = detonationTimer;
    }

    void Spawn()
    {
        spawnSize = transform.localScale;
        transform.localScale /= 10f;
        status = StatusDM.Spawning;
    }
}
