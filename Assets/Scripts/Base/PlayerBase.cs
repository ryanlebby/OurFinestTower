using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    public int maxHealth = 10;

    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        MainPanel.Instance.SetBaseHPDisplay(currentHealth, maxHealth);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {        
        currentHealth -= damage;
        Debug.Log(string.Format("Castle takes {0} damage. {1} / {2}", damage.ToString(), currentHealth.ToString(), maxHealth.ToString()));

        if (currentHealth <= 0)
            KillBase();

        MainPanel.Instance.SetBaseHPDisplay(currentHealth, maxHealth);
    }

    public void KillBase()
    {
        Debug.Log("Base Died!");
        currentHealth = maxHealth;
        MainPanel.Instance.SetBaseHPDisplay(currentHealth, maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            TakeDamage(1);
            other.GetComponent<Unit>().Deactivate();
        }
    }
}
