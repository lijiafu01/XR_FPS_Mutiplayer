using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
        // Gọi phương thức từ InputManager để kiểm tra nếu nút trigger được nhấn
        if (InputManager.Instance.GetTriggerPressed())
        {
            Debug.Log("Người chơi tấn công");
            Attack();
        }
    }
    private void Attack()
    {
        // Xử lý logic tấn công ở đây
    }
}
