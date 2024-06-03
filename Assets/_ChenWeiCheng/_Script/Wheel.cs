using UnityEngine;

public class Wheel : MonoBehaviour
{
    public Transform rightController; // Tham chiếu tới controller bên phải
    private Rigidbody tireRigidbody;
    private bool isGripping = false;
    private bool isCollidingWithTire = false;

    void Start()
    {
        // Lấy tham chiếu tới Rigidbody của lốp xe
        tireRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Kiểm tra nếu nút grip phải đang được nhấn
        isGripping = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (isGripping && isCollidingWithTire)
        {
            // Thiết lập lốp xe là con của controller và vô hiệu hóa vật lý
            transform.SetParent(rightController);
            tireRigidbody.isKinematic = true;
        }
        else if (!isGripping && transform.parent == rightController)
        {
            // Đặt lại parent của lốp xe về null (tầng cao nhất) và kích hoạt vật lý
            transform.SetParent(null);
            tireRigidbody.isKinematic = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == rightController)
        {
            isCollidingWithTire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == rightController)
        {
            isCollidingWithTire = false;
        }
    }
}
