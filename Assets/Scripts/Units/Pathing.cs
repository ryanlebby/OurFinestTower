﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {

    public float speed = 4f;
    public UnitPath Path;

    private int curIndex = 0;
    private Transform target = null;

    void Start()
    {
        Path = GameManager.Instance.GetUnitPath();
        transform.position = Path.At(0).transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (curIndex < Path.Length && this.gameObject.activeSelf)
        {
            if (target == null)
                target = Path.At(curIndex).transform;

            Walk();
        }

        else
        {
            GameManager.Instance.ActiveUnits.Remove(this.GetComponent<Unit>());
            this.gameObject.SetActive(false);
        }
    }    

    void Walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, target.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            curIndex++;
            target = Path.At(curIndex).transform;
        }
    }

    public void Reset()
    {
        target = null;
        curIndex = 0;
        transform.position = Path.At(0).transform.position;
    }
}
