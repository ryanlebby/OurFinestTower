using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMatter : Projectile
{
    public float BaseAttackPower = 0f;    
    public float BaseSpeed = 10f;
    public float SpawnGrowthSpeed = 0.075f;    
    public float DetonateTimer = 2.0f;
    public float DetonateGrowthSpeed = 0.075f;
    public float DetonateRange = 3f;
    public float DetonateDmgMultiplier = 1.5f;
      

    public float CalculatedAttackDamage { get; set; }
    public Unit Target { get; set; }
    public Transform Tower { get; set; }
    public float TowerAttackPower { get; set; }
    public float Range { get; set; }
    public bool IsFired { get; set; }
    public bool IsDetonating { get; set; }

    private Vector3 initialSize;
    private Vector3 spawnSize;    
    private float timer;


    // Use this for initialization
    void Start()
    {
        initialSize = transform.localScale;
        IsFired = false;
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFired && !IsDetonating)
        {
            if (Vector3.Distance(transform.position, Tower.transform.position) > Range 
                || Target == null 
                || !Target.gameObject.activeSelf)
            {
                StartCoroutine("Detonate");
            }

            else
            {
                if (transform.position != Target.transform.position)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        Target.transform.position, 
                        BaseSpeed * Time.deltaTime
                    );
                }
            }
        }       
    }

    public void Reset()
    {
        transform.localScale = initialSize;
        IsFired = false;
        Target = null;
        StartCoroutine("Spawn");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() == Target && !IsDetonating)
        {
            Target.TakeDamage(CalculatedAttackDamage);
            StartCoroutine("Detonate");
        }            
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
                unit.TakeDamage(CalculatedAttackDamage * DetonateDmgMultiplier);
            }
        }

        this.gameObject.SetActive(false);
    }

    IEnumerator Spawn()
    {
        spawnSize = transform.localScale;
        transform.localScale /= 10f;

        while (transform.localScale.x <= spawnSize.x)
        {
            transform.localScale *= 1 + SpawnGrowthSpeed;
            var col = this.gameObject.GetComponentInChildren<Renderer>().material.color;
            col.a = 0.5f;

            yield return null;
        }

        transform.localScale = spawnSize;
    }
}
