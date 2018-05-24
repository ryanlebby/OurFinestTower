using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {
    
    private int curIndex = 0;
    private Vector3 target;
    private bool noTarget = true;
    private Unit unit;
    private Vector3 SpawnLocation;
    private Vector3 SpawnRotation;
    private UnitPath Path;

    void Start()
    {
        Path = GameManager.Instance.GetUnitPath();

        unit = GetComponent<Unit>();
        SpawnLocation = PositionClampY(Path.At(0));       
    }

    // Update is called once per frame
    void Update () {

        // If unit has not reached the end and is still active
        if (curIndex < Path.Length && this.gameObject.activeSelf)
        {
            if (noTarget)
            {
                var pathNode = Path.At(curIndex);
                target = PositionClampY(pathNode);
                noTarget = false;
            }

            Walk();
        }

        // If unit has reached the end or is dead
        else
        {
            GameManager.Instance.ActiveUnits.Remove(unit);
            this.gameObject.SetActive(false);
        }
    }    

    void Walk()
    {
        // Rotate towards the target
        transform.forward = Vector3.RotateTowards(
            transform.forward, 
            target - transform.position, 
            unit.MoveSpeed * Time.deltaTime, 
            0.0f
        );

        // Move towards the target
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target, 
            unit.MoveSpeed * Time.deltaTime
        );

        // If current target has been reached
        if (transform.position == target)
        {
            curIndex++;

            if (curIndex < Path.Length)
            {
                var pathNode = Path.At(curIndex);
                target = PositionClampY(pathNode);
            }                
        }
    }

    public void Reset()
    {
        curIndex = 0;
        target = Vector3.zero;
        noTarget = true;
        transform.position = PositionClampY(Path.At(0));
    }

    private Vector3 PositionClampY(Transform transform)
    {
        return new Vector3(
            transform.position.x,
            unit.YOffset,
            transform.position.z
            );
    }
}
