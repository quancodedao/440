using UnityEngine;
using System.Collections.Generic;
using System;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct PoolObject
    {
        public string id;
        public GameObject prefab;
        public int intialcount;
    }

    public PoolObject[] objectstoPool;
    private Dictionary<string, Queue<GameObject>> objectPool;

    public void Start()
    {
        if (objectPool == null)
        {
            objectPool = new Dictionary<string, Queue<GameObject>>();
        }
        Init();
    }

    public void Init()
    {
        if (objectstoPool == null)
        {
            objectstoPool = new PoolObject[0];
        }

        foreach (var poolObject in objectstoPool)
        {
            if (poolObject.prefab == null)
            {
                Debug.LogError($"Prefab is null for pool ID: {poolObject.id}");
                continue;
            }

            if (!objectPool.ContainsKey(poolObject.id))
            {
                objectPool.Add(poolObject.id, new Queue<GameObject>());
            }

            for (var i = 0; i < poolObject.intialcount; i++)
            {
                var obj = Instantiate(poolObject.prefab, transform); // ✅ Đã sửa dùng prefab
                obj.SetActive(false);
                objectPool[poolObject.id].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(string id)
    {
        if (objectPool.ContainsKey(id) && objectPool[id].Count > 0)
        {
            var obj = objectPool[id].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        var poolObject = Array.Find(objectstoPool, x => x.id == id);
        if (poolObject.prefab == null)
        {
            Debug.LogError($"[ObjectPool] Prefab is null or not found for ID: {id}");
            return null;
        }

        var outPut = Instantiate(poolObject.prefab, transform);
        Debug.LogWarning($"No object available in pool: {id}, creating new one.");
        return outPut;
    }

    public void ReturnObject(string id, GameObject obj)
    {
        if (objectPool.ContainsKey(id))
        {
            obj.SetActive(false);
            objectPool[id].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning($"Attempting to return object to non-existent pool: {id}. Destroying it.");
            Destroy(obj);
        }
    }
}
