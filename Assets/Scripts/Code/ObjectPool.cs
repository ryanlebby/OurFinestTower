using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool {

    private List<GameObject> pool = new List<GameObject>();

    public void Add(GameObject g)
    {
        pool.Add(g);
    }

    public void Remove(GameObject g)
    {
        pool.Remove(g);
    }

    public void Clear()
    {
        pool.Clear();
    }

    public GameObject GetNextAvailable()
    {
        return pool.Where(g => !g.activeSelf).FirstOrDefault();
    }
}
