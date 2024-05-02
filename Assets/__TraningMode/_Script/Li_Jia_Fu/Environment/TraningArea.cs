using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TraningMode;
public class TraningArea : MonoBehaviour
{
    public float radius = 5f; // Bán kính của khu vực kiểm tra

    void Start()
    {
        Invoke("CheckAndRemoveObjects", 1f);
        //CheckAndRemoveObjects();
    }

    void CheckAndRemoveObjects()
    {
        // Sử dụng OverlapSphere để lấy tất cả colliders xung quanh vị trí này với bán kính đã cho
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;

        // Duyệt qua tất cả colliders
        while (i < hitColliders.Length)
        {
            // Kiểm tra nếu collider có tag là "evr"
            if (hitColliders[i].tag == "evr")
            {
                // Xóa đối tượng
                Destroy(hitColliders[i].gameObject);
            }
            i++;
        }
    }

    // Hàm hữu ích để vẽ gizmo trong Editor, giúp bạn dễ hình dung vùng kiểm tra
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
