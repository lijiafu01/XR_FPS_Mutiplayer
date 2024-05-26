using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class Arrow : MonoBehaviour
{
    private Rigidbody rb;  // Reference to the Rigidbody component
    [SerializeField] private float penetrationDepth = 0.5f; // Configurable penetration depth
    private Vector3 lastVelocity; // Biến để lưu vận tốc cuối cùng trước khi va chạm
    private CapsuleCollider bowCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        bowCollider = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        if (bowCollider != null)
        {
            bowCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("bowCollider is not assigned!");
        }

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        else
        {
            Debug.LogError("Rigidbody (rb) is not assigned!");
        }
    }


    private void Update()
    {
        if (rb.velocity != Vector3.zero)
        {
            lastVelocity = rb.velocity.normalized; // Cập nhật hướng của mũi tên
        }
    }
   /* private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BowStringHandle"))
        {
            transform.position = other.transform.position;
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BowCenter"))
        {
            bowCollider.isTrigger = false;
            Invoke("ReturnObjectPool", 5f);
        }
    }

    private void ReturnObjectPool()
    {
       // Destroy(gameObject);
        ObjectPoolManager.Instance.ReturnToPool("arrow", gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("target"))
        {
            Debug.Log("dev co vam cham");
            rb.isKinematic = true; // Đảm bảo Rigidbody không ảnh hưởng đến mô phỏng va chạm

            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 contactNormal = collision.contacts[0].normal;

            // Điều chỉnh vị trí mũi tên để cắm vào mục tiêu
            transform.position = contactPoint - lastVelocity * penetrationDepth;

            // Đặt hướng của mũi tên để thẳng hàng với hướng ban đầu của nó
            transform.forward = lastVelocity;

            // Parent mũi tên vào đối tượng mục tiêu
            transform.SetParent(collision.transform);
        }
    }
}
