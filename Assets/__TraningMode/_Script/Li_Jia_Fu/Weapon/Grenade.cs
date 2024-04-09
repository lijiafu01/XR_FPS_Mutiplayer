using UnityEngine;

public class Grenade : WeaponBehaviour
{
    public GameObject grenadePrefab; // Prefab của lựu đạn
    public Transform throwPoint; // Điểm ném (có thể là vị trí của bộ điều khiển hoặc nhân vật)
    public float throwForce = 10f; // Lực ném

    public override void Fire()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Lấy lực vung và áp dụng nó như là lực ném
        Vector3 throwVelocity = InputManager.Instance.GetControllerVelocity(true); // true cho bàn tay phải
        rb.velocity = throwVelocity * throwForce;

        // Tùy chọn: Thêm một chút xoáy dựa trên vận tốc góc của bộ điều khiển
        Vector3 angularVelocity = InputManager.Instance.GetControllerAngularVelocity(true);
        rb.angularVelocity = angularVelocity;
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
