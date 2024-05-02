using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController =  GetComponentInParent<CharacterController>();
    }

    void Update()
    {

        Movement();
        ChangeDirection();
    }
    private void Movement()
    {
        // Lấy input từ joystick
        float horizontal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        float vertical = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

        // Tính toán hướng di chuyển dựa trên input
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * vertical + right * horizontal).normalized;

        // Di chuyển nhân vật
        characterController.Move(desiredMoveDirection * speed * Time.deltaTime);
    }
    private bool canRotate = true;
    private void ChangeDirection()
    {
        // Lấy input từ joystick bên phải
        float horizontal = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        // Kiểm tra nếu có input và có thể xoay
        if (Mathf.Abs(horizontal) > 0.1f && canRotate)
        {
            float rotateAmount = 45f * Mathf.Sign(horizontal); // Đổi hướng dựa trên input
            transform.parent.transform.Rotate(0, rotateAmount, 0);
            canRotate = false; // Cập nhật biến để không xoay liên tục
        }
        else if (Mathf.Abs(horizontal) < 0.1f) // Khi người chơi thả joystick
        {
            canRotate = true; // Cho phép xoay khi gạt joystick lần tiếp theo
        }
    }
}

