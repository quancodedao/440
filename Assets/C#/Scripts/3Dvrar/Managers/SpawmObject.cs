using System;
using System.Collections;
using UnityEngine;
using Utils;

public class SpawnObject : MonoSingleton<SpawnObject>
{
    private string pooledObjectId;

    private ObjectPool objectPool;

    private float currentX = 0;

    void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    public void Spawn(string pooledObjectId)
    {
        this.pooledObjectId = pooledObjectId;
        var go = objectPool.GetObject(pooledObjectId);
        if (go != null)
        {
            go.transform.position = new Vector3(currentX, 0, 0);
            go.transform.rotation = Quaternion.identity;
            currentX++;
            //Invoke("ReturnObject",go, 1f);
            StartCoroutine(ReturnObject(go));
        }
        else
        {
            Debug.LogWarning($"No object found with ID: {pooledObjectId}");
        }
    }

    private IEnumerator ReturnObject(GameObject go)
    {
        yield return new WaitForSeconds(1f);
        objectPool.ReturnObject(pooledObjectId, go);
        currentX = 0;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Object"))
        {
            Spawn(pooledObjectId);
        }
    }
}