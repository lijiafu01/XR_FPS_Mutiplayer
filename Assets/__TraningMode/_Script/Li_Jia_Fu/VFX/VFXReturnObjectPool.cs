using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class VFXReturnObjectPool : MonoBehaviour
{
    private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    void Start()
    {
        // Lấy tất cả các ParticleSystem trên đối tượng này và con của nó
        GetComponentsInChildren<ParticleSystem>(particleSystems);

        // Bắt đầu Coroutine để kiểm tra khi các ParticleSystem hoàn thành
        StartCoroutine(CheckIfParticlesComplete());
    }

    IEnumerator CheckIfParticlesComplete()
    {
        // Lặp cho đến khi tất cả ParticleSystem không còn hoạt động
        yield return new WaitUntil(() => AreAllParticlesStopped());

        // Khi tất cả các hạt đã phát xong, gọi phương thức để trả đối tượng về object pool
        ReturnToPool();
    }

    bool AreAllParticlesStopped()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps.IsAlive(true))  // true để kiểm tra các hạt con
            {
                return false;  // Nếu bất kỳ ParticleSystem nào vẫn còn hoạt động
            }
        }
        return true;  // Tất cả ParticleSystem đã dừng
    }

    void ReturnToPool()
    {
        // Viết logic để trả về object pool tại đây, ví dụ:
        ObjectPoolManager.Instance.ReturnToPool("pistolmuzzleflash", gameObject);
    }
}
