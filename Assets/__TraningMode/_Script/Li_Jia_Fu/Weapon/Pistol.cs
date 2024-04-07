using UnityEngine;

public class Pistol : WeaponBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;
    

    protected override void Awake()
    {

    }
    protected override void Start()
    {

    }


    public override void Reload()
    {

    }
    public override void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(muzzle.forward * 700f);
            nextFireTime = Time.time + 1f / fireRate; // Cập nhật thời gian bắn tiếp theo dựa trên fireRate
            Debug.Log("Đã bắn 1 viên đạn");
        }
    }
    public override void FillAmmunition(int amount)
    {
    }
}
