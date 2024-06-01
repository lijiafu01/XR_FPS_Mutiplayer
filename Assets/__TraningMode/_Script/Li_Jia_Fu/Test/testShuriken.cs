using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShuriken : MonoBehaviour
{
    public float speed = 5.0f;          // Tốc độ di chuyển của shuriken
    public float maxDistance = 10.0f;   // Khoảng cách tối đa mà shuriken sẽ bay ra
    public float rotationSpeed = 360.0f; // Tốc độ xoay mỗi giây

    private Vector3 originalPosition;   // Vị trí ban đầu của shuriken
    private bool isReturning = false;   // Trạng thái có đang quay trở lại không

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(ThrowAndReturn());
        }

        // Xoay shuriken
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ThrowAndReturn()
    {
        // Tính toán vị trí đích dựa trên khoảng cách tối đa
        Vector3 targetPosition = originalPosition + transform.right * maxDistance;

        // Di chuyển đến điểm đích
        while (!isReturning && Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Điều chỉnh trạng thái để bắt đầu quay trở lại
        isReturning = true;

        // Di chuyển trở lại vị trí ban đầu
        while (isReturning && Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Đặt lại trạng thái
        isReturning = false;
    }
}
