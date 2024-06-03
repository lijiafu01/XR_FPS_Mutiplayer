using UnityEngine;

public class NewWheel : MonoBehaviour
{
    public Transform rightController; // Tham chiếu tới controller bên phải
    private bool isCollidingWithNewWheel = false;
    private GameObject collidedObject;
    private bool isGripping = false;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Lấy tham chiếu tới MeshRenderer của NewWheel
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false; // Bắt đầu với MeshRenderer bị tắt
        }
    }

    void Update()
    {
        // Kiểm tra nếu nút grip phải đang được nhấn
        isGripping = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (!isGripping && isCollidingWithNewWheel)
        {
            // Bật MeshRenderer và xóa đối tượng va chạm
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }

            if (collidedObject != null)
            {
                Destroy(collidedObject);
            }

            isCollidingWithNewWheel = false; // Reset trạng thái va chạm
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewWheel"))
        {
            isCollidingWithNewWheel = true;
            collidedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NewWheel"))
        {
            isCollidingWithNewWheel = false;
            collidedObject = null;
        }
    }
}
