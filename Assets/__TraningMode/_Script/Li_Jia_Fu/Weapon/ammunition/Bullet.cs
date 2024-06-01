using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
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
