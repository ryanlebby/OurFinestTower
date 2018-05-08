using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public float MoveSpeed = 6f;
    public float MaxHealth = 20f;    
    public Image HealthBar;

    public float Health { get; set; }
    //private Vector3 initialSize;

    // Use this for initialization
    void Start () {
        Health = MaxHealth;
        //initialSize = transform.localScale;
        //transform.localScale *= MaxHealth / 200.0f;
    }

    public void Reset()
    {
        Health = MaxHealth;
        //transform.localScale = initialSize;
        //transform.localScale *= MaxHealth / 100.0f;
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
