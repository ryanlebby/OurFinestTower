using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolType
{
    List,
    Dictionary
}

public class PoolDict
{
    private Dictionary<string, GameObject> pool = new Dictionary<string, GameObject>();

    public int Count { get { return pool.Count; } }

    public void Add(string key, GameObject gameObject)
    {
        pool.Add(key, gameObject);
    }

    public void Remove(string key)
    {
        pool.Remove(key);
    }

    public void Clear()
    {
        pool.Clear();
    }

    public GameObject GetObject(string key)
    {
        return pool[key];
    }
}

public class PoolList {

    protected List<GameObject> pool = new List<GameObject>();

    public int Count { get { return pool.Count; } }

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

public class UnitPool : PoolList
{
    public GameObject GetByName(string name)
    {
        return pool
            .Where(g => g.GetComponent<Unit>().Name == name && !g.activeSelf)
            .FirstOrDefault();
    }
}
