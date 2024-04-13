using UnityEngine;

public class Pistol : WeaponBehaviour
{
    
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float shootForce = 800f;
    [SerializeField] private GameObject smokeEffectPrefab;
    [SerializeField] private GameObject bulletPrefab;


    protected override void Awake()
    {

    }
    protected override void Start()
    {

    }
    public override void Reload()
    {
        if (!InputManager.Instance.IsRightGripPressed()) return;
        Debug.Log("dev Reload pistols");
    }
    public override void Fire()
    {
        if (!InputManager.Instance.GetTriggerPressed()) return;
        if (Time.time >= nextFireTime)
        {
            //GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("pistolbullet",muzzle.position,muzzle.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(muzzle.forward * shootForce);

            nextFireTime = Time.time + 1f / fireRate; // Cập nhật thời gian bắn tiếp theo dựa trên fireRate

            if (smokeEffectPrefab != null)
            {
                GameObject muzzleFlash = Instantiate(smokeEffectPrefab, muzzle.position, muzzle.rotation);
                Destroy(muzzleFlash,0.2f);
            }
        }
    }
    public override void FillAmmunition(int amount)
    {
    }
}
