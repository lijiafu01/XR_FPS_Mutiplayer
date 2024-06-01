using UnityEngine;
using TraningMode;
public class Pistol : WeaponBehaviour
{
    
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float shootForce = 800f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource shootSound;  // AudioSource for shooting the pistol
    [SerializeField] private Animator Anim;
    protected override void Awake()
    {

    }
    protected override void Start()
    {

    }
    protected override void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward * 30, Color.red);
    }
    public override void Reload()
    {
        if (!InputManager.Instance.IsRightGripPressed()) return;
        Debug.Log("dev Reload pistols");
    }
    public override void Fire()
    {
        
        if (!InputManager.Instance.GetTriggerPressed()) return;
        if (!GameManager.Instance.isRun) return;
        if (Time.time >= nextFireTime)
        {
            //GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("pistolbullet",muzzle.position,muzzle.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(muzzle.forward * shootForce);

            nextFireTime = Time.time + 1f / fireRate; // Cập nhật thời gian bắn tiếp theo dựa trên fireRate

            GameObject hitVFXInstance = ObjectPoolManager.Instance.SpawnFromPool("pistolmuzzleflash", muzzle.position, muzzle.rotation);
            shootSound.Play();
            Anim.SetTrigger("shoot");
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
        }
    }
    public override void FillAmmunition(int amount)
    {
    }
}
