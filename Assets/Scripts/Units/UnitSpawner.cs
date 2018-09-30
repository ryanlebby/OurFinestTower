
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    public float MinSpawnTimer = 0.5f;
    public float MaxSpawnTimer = 2.5f;
    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public UnitPath Path;

    private UnitPool UnitPool = new UnitPool();
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
        SpawnTimer = Random.Range(MinSpawnTimer, MaxSpawnTimer);
    }

    // True = store units in UnitPool for re-use
    // False = always create a new instance. Use ONLY IN TESTING!
    public void SpawnUnit(bool enableUnitPooling = true)
    {
        GameObject unitGO;
        var unitName = RandomUnitName();

        if (enableUnitPooling)
        {            
            unitGO = UnitPool.GetByName(unitName);

            if (unitGO != null)
            {
                unitGO.GetComponent<Unit>().Reset();
                unitGO.GetComponent<Pathing>().Reset();
                unitGO.transform.LookAt(Path.At(0));
                unitGO.SetActive(true);                
            }

            else
            {
                var prefab = UnitPrefabs
                    .Where(u => u.GetComponent<Unit>().Name == unitName)
                    .SingleOrDefault();

                unitGO = Instantiate(prefab, transform.position, Quaternion.identity);
                unitGO.GetComponent<Pathing>().Path = Path;
                unitGO.transform.LookAt(Path.At(0));
                unitGO.transform.parent = this.transform;
                UnitPool.Add(unitGO);
            }
        }

        else
        {
            var prefab = UnitPrefabs
                    .Where(u => u.GetComponent<Unit>().Name == unitName)
                    .SingleOrDefault();

            unitGO = Instantiate(prefab, transform.position, Quaternion.identity);
            unitGO.GetComponent<Pathing>().Path = Path;
            unitGO.transform.LookAt(Path.At(0));
            unitGO.transform.parent = this.transform;
        }

        GameManager.Instance.ActiveUnits.Add(unitGO.GetComponent<Unit>());
        //Debug.Log(System.DateTime.Now.ToString() + "  Spawned " + unitGO.GetComponent<Unit>().Name);
    }

    private string RandomUnitName()
    {
        var totalRarity = UnitPrefabs.Sum(u => u.GetComponent<Unit>().Rarity);
        var rarityThreshold = Random.Range(0, totalRarity);

        int curRarityCount = 0;
        int curIndex = -1;
        Unit curUnit = null;

        while (curRarityCount < rarityThreshold)
        {
            curIndex++;
            curUnit = UnitPrefabs[curIndex].GetComponent<Unit>();
            curRarityCount += curUnit.Rarity;            
        }

        return curUnit.Name;
    }
}
