using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Unit[] units;
    public List<Unit> ActiveUnits = new List<Unit>();
    public List<UnitPath> Paths;

    public static GameManager Instance;

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public UnitPath GetUnitPath()
    {
        return Paths[Random.Range(0, Paths.Count)];
    }
}
