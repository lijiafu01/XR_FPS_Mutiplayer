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

        for (int i = 0; i < additionalCount; i++)
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

        // Cập nhật lại dictionary với hàng đợi mới
        poolDictionary[tag] = objectPool;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion? rotation = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Kiểm tra xem tất cả các đối tượng trong pool có đang active không
        if (AllObjectsActive(tag))
        {
            ExpandPool(tag, 1);  // Mở rộng pool nếu tất cả đối tượng đều active, thêm 1 đối tượng mới
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
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)  // Kiểm tra nếu có bất kỳ đối tượng nào không active
            {
                return false;
            }
        }
        return true;  // Tất cả đối tượng đều active
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
