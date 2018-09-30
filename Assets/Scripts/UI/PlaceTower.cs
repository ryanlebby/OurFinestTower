using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaceTower : MonoBehaviour {

    public GameObject TowerParentObject;

    private bool placeMode;
    private GameObject towerPrefab;
    private float rayDistance = 250f;

    void Start()
    {
        placeMode = false;    
    }

    private void Update()
    {
        if (placeMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask towerBases = 1 << LayerMask.NameToLayer("TowerBase");

                if (Physics.Raycast(ray, out hit, rayDistance, towerBases))
                {
                    var towerBase = hit.collider.gameObject.GetComponent<TowerBase>();

                    if (!towerBase.HasTower)
                    {
                        Vector3 towerSpawnPoint = towerBase.Position;

                        //var prefabs = GameManager.Instance.Prefabs_Towers;
                        //var tower = prefabs[Random.Range(0, prefabs.Count)];

                        GameObject go = Instantiate(towerPrefab, towerSpawnPoint, Quaternion.identity);
                        go.transform.parent = TowerParentObject.transform;

                        towerBase.HasTower = true;
                        towerPrefab = null;
                        placeMode = false;
                    }
                    
                }
            }
        }
    }

    public void EnterPlacementMode(string towerName)
    {
        placeMode = true;
        towerPrefab = GameManager.Instance.Prefabs_Towers
            .Where(t => t.GetComponentInChildren<Tower>().name == towerName)
            .SingleOrDefault();

        Debug.Log("Placement Mode: " + towerName + ((towerPrefab == null) ? "  ERROR" : "  SUCCESS"));
    }
}
