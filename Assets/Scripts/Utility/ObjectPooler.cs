using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount = 10;
    public bool willGrow = true;

    List<GameObject> objects;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                return objects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            obj.SetActive(false);
            objects.Add(obj);
            return obj;
        }

        return null;
    }
}
