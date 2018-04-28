using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public float SpawnRate = 3.0f;

    private List<Unit> UnitSupply = new List<Unit>();

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnUnit", 2.0f, SpawnRate);
	}
	
	public void SpawnUnit()
    {
        var unit = GetNextAvailableUnit();
        GameObject go;

        if (unit != null)
        {
            go = unit.gameObject;
            unit.transform.position = transform.position;
            unit.Reset();
            unit.GetComponent<Pathing>().Reset();
        }
        else
        {
            go = Instantiate(UnitPrefabs[Random.Range(0, UnitPrefabs.Count)], transform.position + Vector3.up, Quaternion.identity);
            unit = go.GetComponent<Unit>();
            UnitSupply.Add(unit);
        }

        unit.enabled = true;
        go.transform.parent = this.transform;
        GameManager.Instance.ActiveUnits.Add(unit);

    }

    private Unit GetNextAvailableUnit()
    {
        return UnitSupply.Where(u => u.enabled == false).FirstOrDefault();
    }
}
