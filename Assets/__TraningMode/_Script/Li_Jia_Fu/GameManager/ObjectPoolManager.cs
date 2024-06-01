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
                obj.transform.parent = poolHolder.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary[pool.tag] = objectPool;
        }
    }

    private GameObject ExpandPoolAndGetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        Pool pool = pools.Find(p => p.tag == tag);
        if (pool == null)
        {
            Debug.LogError("No pool definition found for tag " + tag);
            return null;
        }

        Transform poolHolder = GameObject.Find(tag + " Pool").transform;
        if (poolHolder == null)
        {
            Debug.LogError("No pool holder found for tag " + tag);
            return null;
        }

        GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(poolHolder);
        obj.SetActive(false);

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion? rotation = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (AllObjectsActive(tag))
        {
            GameObject newObject = ExpandPoolAndGetObject(tag);
            newObject.SetActive(true);
            newObject.transform.position = position;
            newObject.transform.rotation = rotation ?? Quaternion.identity;
            return newObject;
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

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        // Kiểm tra và thiết lập lại đối tượng cha của các đối tượng con
        foreach (Transform child in objectToReturn.transform)
        {
            if (poolDictionary.ContainsKey(child.gameObject.tag))
            {
                // Thiết lập lại đối tượng cha của đối tượng con về holder của pool tương ứng
                Transform poolHolder = GameObject.Find(child.gameObject.tag + " Pool").transform;
                child.SetParent(poolHolder);
                child.gameObject.SetActive(false);
                poolDictionary[child.gameObject.tag].Enqueue(child.gameObject);
            }
        }

        // Reset các thuộc tính vật lý của đối tượng trả về pool
        Rigidbody rb = objectToReturn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
        }

        // Đặt lại vị trí và hướng của đối tượng
        objectToReturn.transform.position = Vector3.zero;
        objectToReturn.transform.rotation = Quaternion.identity;

        // Đặt lại đối tượng cha của objectToReturn
        Transform parentPoolHolder = GameObject.Find(tag + " Pool").transform;
        objectToReturn.transform.SetParent(parentPoolHolder);
        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }


}
