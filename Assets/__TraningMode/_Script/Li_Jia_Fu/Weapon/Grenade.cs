using UnityEngine;
using TraningMode;
public class Grenade : WeaponBehaviour
{
    
    public GameObject grenadePrefab; // Prefab của lựu đạn
    public Transform throwPoint; // Điểm ném (có thể là vị trí của bộ điều khiển hoặc nhân vật)
    public float throwForce = 10f; // Lực ném

    public float throwCooldown = 1f; // Thời gian chờ giữa các lần ném

    private float lastThrowTime = -Mathf.Infinity; // Thời điểm ném cuối cùng
    public override void Fire()
    {
        if (!InputManager.Instance.IsRightGripPressed()) return;
        // Kiểm tra cooldown
        if (Time.time - lastThrowTime < throwCooldown) return;

        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Lấy lực vung và áp dụng nó như là lực ném
        Vector3 throwVelocity = InputManager.Instance.GetControllerVelocity(true); // true cho bàn tay phải
        rb.velocity = throwVelocity * throwForce;

        // Tùy chọn: Thêm một chút xoáy dựa trên vận tốc góc của bộ điều khiển
        Vector3 angularVelocity = InputManager.Instance.GetControllerAngularVelocity(true);
        rb.angularVelocity = angularVelocity;

        // Cập nhật thời gian ném cuối cùng
        lastThrowTime = Time.time;
    }



    public override void Reload()
    {
        // Lựu đạn có thể không cần phương thức Reload
    }

    public override void FillAmmunition(int amount)
    {
        // Lựu đạn có thể không cần phương thức FillAmmunition
    }
}
