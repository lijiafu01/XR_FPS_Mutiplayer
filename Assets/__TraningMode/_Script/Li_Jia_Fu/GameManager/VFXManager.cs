using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }
    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PistolMuzzelVFX()
    {

    }
    public void WoodHitBig(Collision collision)
    {
        // Tạo hitVFX tại điểm va chạm
        // Đảo ngược hướng của vector pháp tuyến và sử dụng nó để quay hitVFXInstance
        GameObject hitVFXInstance = ObjectPoolManager.Instance.SpawnFromPool("woodhitbig", collision.contacts[0].point, Quaternion.LookRotation(-collision.contacts[0].normal));

        ParticleSystem ps = hitVFXInstance.GetComponent<ParticleSystem>() ?? hitVFXInstance.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            StartCoroutine(ReturnVFXToPoolAfterFinished(ps, hitVFXInstance));
        }
    }

    private IEnumerator ReturnVFXToPoolAfterFinished(ParticleSystem ps, GameObject vfxInstance)
    {
        // Đợi cho đến khi Particle System hoàn thành
        yield return new WaitForSeconds(ps.main.duration);

        // Trả GameObject về pool
        ObjectPoolManager.Instance.ReturnToPool("woodhitbig", vfxInstance);
    }
}
