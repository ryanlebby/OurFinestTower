using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    public int maxHealth = 10;

    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        UpdateHPDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        Debug.Log(string.Format("Castle takes {0} damage. {1} / {2}", damage.ToString(), currentHealth.ToString(), maxHealth.ToString()));
        currentHealth -= damage;
        if (currentHealth <= 0)
            KillBase();

        UpdateHPDisplay();
    }

    public void KillBase()
    {
        Debug.Log("Base Died!");
        currentHealth = maxHealth;
    }

    public void UpdateHPDisplay()
    {
        var mp = MainPanel.Instance;
        MainPanel.Instance.baseHPDisplay.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
