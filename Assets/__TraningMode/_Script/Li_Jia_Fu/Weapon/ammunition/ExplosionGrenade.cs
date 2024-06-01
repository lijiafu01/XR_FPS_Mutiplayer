using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class ExplosionGrenade : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    bool ischeck =false;
    public GameObject explosionEffect; // Gán prefab cha chứa ParticleSystem trong Inspector
    private void Start()
    {
        Invoke("TriggerExplosion",3f);
    }
    // Phương thức để kích hoạt vụ nổ
    public void TriggerExplosion()
    {
        PlaySound();
        // Tạo hiệu ứng tại vị trí của đối tượng này
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // Kích hoạt tất cả các Particle Systems trong prefab
        foreach (ParticleSystem ps in explosion.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
        ischeck = true;
        
    }
    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            // Hủy GameObject sau khi âm thanh phát xong
            Destroy(gameObject, audioSource.clip.length);
        }
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
                DummyTarget dummy = hitCollider.GetComponent<DummyTarget>();
                dummy.GrenadeCollider();
            }
        }
    }
    
}
