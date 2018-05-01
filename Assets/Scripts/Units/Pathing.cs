using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {

    public UnitPath Path;

    private int curIndex = 0;
    private Transform target = null;
    private Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
        Path = GameManager.Instance.GetUnitPath();
        transform.position = Path.At(0).transform.position;
    }

    // Update is called once per frame
    void Update () {

        // If unit has not reached the end and is still active
        if (curIndex < Path.Length && this.gameObject.activeSelf)
        {
            if (target == null)
                target = Path.At(curIndex).transform;

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
            target.position - transform.position, 
            unit.MoveSpeed * Time.deltaTime, 
            0.0f
        );

        // Move towards the target
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.position, 
            unit.MoveSpeed * Time.deltaTime
        );

        // If current target has been reached
        if (transform.position == target.position)
        {
            curIndex++;

            if (curIndex < Path.Length)
                target = Path.At(curIndex).transform;
        }
    }

    public void Reset()
    {
        curIndex = 0;
        target = null;        
        transform.position = Path.At(0).transform.position;
    }
}
