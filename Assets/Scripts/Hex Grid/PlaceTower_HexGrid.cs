using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaceTower_HexGrid : MonoBehaviour
{

    public HexGrid grid;

    private bool placeMode;
    private GameObject towerPrefab;
    private GameObject movingTower;
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
                    var hexSlot = hit.collider.gameObject.GetComponent<SnapToHexGrid>().hexSlot;

                    if (!towerBase.HasTower)
                    {
                        Vector3 towerSpawnPoint = towerBase.Position;
                        
                        GameObject gameObject = Instantiate(towerPrefab, towerSpawnPoint, Quaternion.identity);
                        grid.AddChild(gameObject.transform);
                        hexSlot.Contents.Add(gameObject);
                        gameObject.GetComponent<SnapToHexGrid>().hexSlot = hexSlot;

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

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

        if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
        {
            //var nearestHexSlot = hit.point
            //movingTower = Instantiate(towerPrefab, )
        }
            
    }
}
