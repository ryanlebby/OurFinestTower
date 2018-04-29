using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public float SpawnRate = 3.0f;

    private ObjectPool UnitPool = new ObjectPool();

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnUnit", 2.0f, SpawnRate);
	}
	
	public void SpawnUnit()
    {
        GameObject go = UnitPool.GetNextAvailable();

        if (go != null)
        {         
            go.GetComponent<Pathing>().Reset();
            go.GetComponent<Unit>().Reset();
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(UnitPrefabs[Random.Range(0, UnitPrefabs.Count)], transform.position + Vector3.up, Quaternion.identity);
            go.transform.parent = this.transform;
            UnitPool.Add(go);           
        }
        
        GameManager.Instance.ActiveUnits.Add(go.GetComponent<Unit>());
    }
}
