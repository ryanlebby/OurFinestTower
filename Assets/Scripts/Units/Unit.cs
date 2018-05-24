using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public string Name;
    public float MoveSpeed = 6f;
    public float BaseAnimatorSpeed = 4.5f;
    public float MaxHealth = 20f;    
    public float YOffset = 0;
    public int Rarity = 100;
    public Transform ProjectileContactPoint;
    public Image HealthBar;

    public float Health { get; set; }

    // Use this for initialization
    void Start () {
        Health = MaxHealth;
        GetComponent<Animator>().speed = MoveSpeed / BaseAnimatorSpeed;
        transform.position += new Vector3(0, 1, 0) * YOffset;
    }

    public void Reset()
    {
        Health = MaxHealth;
        HealthBar.fillAmount = 1;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        HealthBar.fillAmount = Health / MaxHealth;

        if (Health <= 0)
        {
            var u = this.GetComponent<Unit>();
            GameManager.Instance.ActiveUnits.Remove(u);
            this.gameObject.SetActive(false);
        }            
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);

        if (active)
        {
            Reset();
            this.GetComponentInParent<Pathing>().Reset();
        }
    }
}
