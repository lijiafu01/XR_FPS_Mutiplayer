using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public Animator anim;
    public int enemyHP;
    public int enemyMaxHP;
    public GameObject hitVFXPrefab; // Đổi tên biến và sử dụng như một Prefab

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            TrainingMission trainingMission = transform.GetComponentInParent<TrainingMission>();
            trainingMission.UpdateMissionProgress(1);

            UIController.Instance.ShowScore(1);
            anim.SetTrigger("hit");

            // Tạo hitVFX tại điểm va chạm
            // Đảo ngược hướng của vector pháp tuyến và sử dụng nó để quay hitVFXInstance
            GameObject hitVFXInstance = Instantiate(hitVFXPrefab, collision.contacts[0].point, Quaternion.LookRotation(-collision.contacts[0].normal));


            // Tìm và kích hoạt ParticleSystem trên bản sao hitVFX
            ParticleSystem ps = hitVFXInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
            else
            {
                // Trong trường hợp ParticleSystem nằm trong một child GameObject của hitVFXInstance
                ps = hitVFXInstance.GetComponentInChildren<ParticleSystem>();
                if (ps != null) ps.Play();
            }

            // Tự hủy hitVFX sau khi đã phát xong
            Destroy(hitVFXInstance, ps.main.duration);
        }
    }
}
