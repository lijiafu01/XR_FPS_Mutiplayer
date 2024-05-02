using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class watermelonTarget : MonoBehaviour
{
    public Animator anim;

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
            VFXManager.Instance.WoodHitBig(collision);
            StartCoroutine(WaitForAnimation("hit"));            
        }
    }
    IEnumerator WaitForAnimation(string stateName)
    {
        anim.SetTrigger(stateName);
        // Chờ cho đến khi trạng thái cần quan tâm bắt đầu chạy
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;  // Đợi cho đến frame tiếp theo trước khi kiểm tra lại
        }

        // Bây giờ trạng thái đã bắt đầu, chờ cho đến khi nó kết thúc
        while (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;  // Đợi cho đến frame tiếp theo trước khi kiểm tra lại
        }

        TrainingMission trainingMission = transform.GetComponentInParent<TrainingMission>();
        trainingMission.UpdateMissionProgress(1);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "objline")
        {
            moveSpeed = -moveSpeed;
        }
    }

}
