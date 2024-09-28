using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_ReceiveHit : MonoBehaviour
{
    public GameObject[] hits;
    private int hitsLength;

    void Start()
    {
        hitsLength = hits.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveHit(RaycastHit hit)
    {
        int randomHit = UnityEngine.Random.Range(0, hitsLength);
        var hitPrefab = Instantiate(hits[randomHit], hit.point, Quaternion.LookRotation(hit.normal));
        float timer = hitPrefab.GetComponent<ParticleSystem>().main.duration;
        Destroy(hitPrefab, timer);
    }
}
