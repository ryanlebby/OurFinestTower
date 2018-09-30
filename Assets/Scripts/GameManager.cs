using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<Unit> ActiveUnits = new List<Unit>();
    public List<UnitPath> Paths;
    public List<GameObject> Prefabs_Towers;

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
        if (Paths.Count == 0)
            return null;

        return Paths[Random.Range(0, Paths.Count)];
    }
}
