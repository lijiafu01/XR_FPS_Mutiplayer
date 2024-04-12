using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                if (pool.tag == "DamageText")
                {
                    obj.transform.SetParent(transform, false);
                }
                else
                {
                    obj.transform.parent = transform;

                }
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Th?m h?ng ??i n?y v?o t? ?i?n pool
            poolDictionary[pool.tag] = objectPool;
        }


    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion? rotation = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation ?? Quaternion.identity;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


    // Call this method to return the object back to the pool
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }


}
