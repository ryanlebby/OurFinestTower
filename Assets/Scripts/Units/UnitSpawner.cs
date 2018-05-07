using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public float MinSpawnRate = 2.5f;
    public float MaxSpawnRate = 0.2f;

    private ObjectPool UnitPool = new ObjectPool();
    private float SpawnTimer;

    private void Update()
    {
        SpawnTimer -= Time.deltaTime;

        if (SpawnTimer < 0)
        {
            SpawnUnit();
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        SpawnTimer = Random.Range(MaxSpawnRate, MinSpawnRate);
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
