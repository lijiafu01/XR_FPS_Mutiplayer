using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonTarget : MonoBehaviour
{
    public Animator anim;
    public int enemyHP;
    public int enemyMaxHP;
    public GameObject hitVFXPrefab; // Đổi tên biến và sử dụng như một Prefab

    //target status
    public float moveSpeed = 5.0f; // Tốc độ di chuyển
    public float rotateSpeed = 90.0f; // Tốc độ quay quanh trục Y (độ/giây)

    public bool isMoving; // Kiểm soát việc di chuyển
    public bool isRotating; // Kiểm soát việc quay
    public bool isMovingAndRotating; // Kiểm soát việc vừa quay vừa di chuyển

    private Vector3 startPosition; // Lưu vị trí khởi đầu để tính toán di chuyển

    void Start()
    {
        startPosition = transform.position; // Cập nhật điểm khởi đầu từ vị trí hiện tại của đối tượng
    }

    void Update()
    {
        if (isMovingAndRotating)
        {
            HandleMovingAndRotating();
        }
        else
        {
            if (isMoving)
            {
                HandleMovement();
            }

            if (isRotating)
            {
                HandleRotation();
            }
        }
    }

    private void HandleMovement()
    {
        // Tính toán góc dựa trên vị trí hiện tại và tốc độ
        float angle = Mathf.Atan2(transform.position.z, transform.position.x) * Mathf.Rad2Deg;
        angle += moveSpeed * Time.deltaTime;

        // Tính toán vị trí mới
        float radius = Vector3.Distance(startPosition, Vector3.zero);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        transform.position = new Vector3(x, transform.position.y, z);

        // Nhìn về tâm (0, 0)
        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }

    private void HandleRotation()
    {
        // Quay quanh trục Y
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
    }

    private void HandleMovingAndRotating()
    {
        // Tương tự như HandleMovement nhưng không nhìn về tâm
        float angle = Mathf.Atan2(transform.position.z, transform.position.x) * Mathf.Rad2Deg;
        angle += moveSpeed * Time.deltaTime;

        float radius = Vector3.Distance(startPosition, Vector3.zero);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        transform.position = new Vector3(x, transform.position.y, z);

        // Xử lý quay đồng thời
        HandleRotation();
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "bullet")
        {

            TrainingMission trainingMission = transform.GetComponentInParent<TrainingMission>();
            trainingMission.UpdateMissionProgress(1);

            UIController.Instance.ShowMissionProgress(trainingMission.targetTotal,1);
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
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "objline")
        {
            moveSpeed = -moveSpeed;
        }
    }

}
