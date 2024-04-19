using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGrenade : MonoBehaviour
{
    bool ischeck =false;
    public GameObject explosionEffect; // Gán prefab cha chứa ParticleSystem trong Inspector
    private void Start()
    {
        Invoke("TriggerExplosion",3f);
    }
    // Phương thức để kích hoạt vụ nổ
    public void TriggerExplosion()
    {
        // Tạo hiệu ứng tại vị trí của đối tượng này
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // Kích hoạt tất cả các Particle Systems trong prefab
        foreach (ParticleSystem ps in explosion.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
        ischeck = true;
    }
    private void Update()
    {
        if (!ischeck) return;
        PerformSphereCast();
    }

    void PerformSphereCast()
    {
        // Tạo một hình cầu ảo tại vị trí hiện tại của GameObject với bán kính nhất định
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("target"))
            {
                Animator anim = hitCollider.transform.gameObject.GetComponent<watermelonTarget>().anim;
                anim.SetTrigger("hit");
                UIController.Instance.ShowMissionProgress(999,1);
            }
        }
       Destroy(gameObject);
    }
    
}
