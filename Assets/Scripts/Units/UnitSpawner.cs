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
        GameObject go = Instantiate(RandomPrefab(), transform.position, Quaternion.identity);
        go.transform.parent = this.transform;

        //GameObject go = UnitPool.GetNextAvailable();

        //if (go != null)
        //{         
        //    go.GetComponent<Pathing>().Reset();
        //    go.GetComponent<Unit>().Reset();
        //    go.SetActive(true);
        //}
        //else
        //{
        //    go = Instantiate(RandomPrefab(), transform.position, Quaternion.identity);
        //    go.transform.parent = this.transform;
        //    UnitPool.Add(go);           
        //}
        
        GameManager.Instance.ActiveUnits.Add(go.GetComponent<Unit>());
    }

    private GameObject RandomPrefab()
    {
        var totalRarity = UnitPrefabs.Sum(u => u.GetComponent<Unit>().Rarity);
        var random = Random.Range(0, totalRarity);

        int curVal = 0;
        int curIndex = -1;

        while (curVal < random)
        {
            curIndex++;
            curVal += UnitPrefabs[curIndex].GetComponent<Unit>().Rarity;            
        }

        return UnitPrefabs[curIndex];
    }
}
