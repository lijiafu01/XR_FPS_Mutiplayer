using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TraningMode;
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
            GameObject poolHolder = new GameObject(pool.tag + " Pool");
            poolHolder.transform.SetParent(transform);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                obj.transform.parent = poolHolder.transform;  // Set parent to the specific pool container
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Thêm hàng đợi này vào từ điển pool
            poolDictionary[pool.tag] = objectPool;
        }
    }
    private void ExpandPool(string tag, int additionalCount = 1)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        Pool pool = pools.Find(p => p.tag == tag);
        if (pool == null)
        {
            Debug.LogError("No pool definition found for tag " + tag);
            return;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];

        // Tìm container phù hợp cho pool này dựa trên tag
        Transform poolHolder = GameObject.Find(tag + " Pool").transform;
        if (poolHolder == null)
        {
            Debug.LogError("No pool holder found for tag " + tag);
            return;
        }

        for (int i = 0; i < additionalCount; i++)
        {
            GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
            obj.transform.SetParent(poolHolder);  // Set parent to the specific pool container

            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        // Không cần cập nhật lại dictionary vì hàng đợi là một tham chiếu
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion? rotation = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Mở rộng pool nếu cần
        if (AllObjectsActive(tag))
        {
            ExpandPool(tag, 1);  // Mở rộng pool nếu tất cả đối tượng đều active
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation ?? Quaternion.identity;
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    private bool AllObjectsActive(string tag)
    {
        return poolDictionary[tag].All(obj => obj.activeInHierarchy);
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
