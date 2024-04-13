using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnEnable()
    {
        Invoke("ReturnObjectPool", 5f);
    }
    private void ReturnObjectPool()
    {
        ObjectPoolManager.Instance.ReturnToPool("pistolbullet", transform.gameObject);
    }
}
