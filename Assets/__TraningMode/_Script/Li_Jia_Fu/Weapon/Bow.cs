using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform bowHandle;       // Điểm nắm giữa dây cung
    [SerializeField] private GameObject arrowPrefab;    // Prefab của mũi tên
    [SerializeField] private LineRenderer bowString;    // LineRenderer để vẽ dây cung
    [SerializeField] private float maxPullDistance = -0.5f; // Khoảng cách kéo tối đa, giá trị âm
    [SerializeField] private float pullStrengthMultiplier = 1000f; // Nhân số để tính lực bắn dựa trên độ kéo
    private Vector3 originalHandlePosition;             // Vị trí ban đầu của bow handle
    private GameObject currentArrow;
    private bool isStringPulled = false;
    private Transform leftHand;                        // Vị trí của controller tay trái

    void Start()
    {
        originalHandlePosition = bowHandle.position;      
        //Invoke("test", 3f);
        ResetString();
    }
   /* void test()
    {
        Vector3 newpos = new Vector3(bowHandle.position.x, bowHandle.position.y, bowHandle.position.z - 0.3f);
        bowHandle.position = newpos;
    }*/
    void Update()
    {
        //viết một cái test ở đây ,nếu tôi nhấn phím k thì bowHandle sẽ từ từ tăng tức là trục z của nó sẽ âm dần cho đến khi maxPullDistance
        //và khi tôi nhả tay ra thì sẽ bắn mũi tên 
        // Kiểm tra nếu phím K được nhấn và dây cung chưa được kéo tối đa
        if (Input.GetKey(KeyCode.K) && bowHandle.position.z > originalHandlePosition.z + maxPullDistance)
        {
            if (!isStringPulled)
            {
                CreateArrow();
                isStringPulled = true;
            }

            // Tính toán và cập nhật vị trí mới cho bowHandle, di chuyển nó từ từ về phía trước
            float newZ = Mathf.Max(bowHandle.position.z - 0.01f, originalHandlePosition.z + maxPullDistance);
            bowHandle.position = new Vector3(bowHandle.position.x, bowHandle.position.y, newZ);
        }
        else if (Input.GetKeyUp(KeyCode.K) && isStringPulled) // Khi phím K được thả ra
        {
            ShootArrow();
            isStringPulled = false;
        }
        else if (!Input.GetKey(KeyCode.K) && !isStringPulled) // Khi không có tương tác
        {
            ResetString();
        }



        // Kiểm tra nút grip của tay cầm trái
        /*if (leftHand != null && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            if (!isStringPulled)
            {
                CreateArrow();
                isStringPulled = true;
            }

            // Kéo dây cung dựa trên vị trí của tay trái trên trục Z
            Vector3 leftHandPos = leftHand.position;
            float pullDistance = Mathf.Min(0, Mathf.Max(maxPullDistance, leftHandPos.z - originalHandlePosition.z));
            bowHandle.position = new Vector3(bowHandle.position.x, bowHandle.position.y, originalHandlePosition.z + pullDistance);
        }
        else if (isStringPulled)
        {
            ShootArrow();
            isStringPulled = false;
            leftHand = null;
        }
        else
        {
            ResetString();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            leftHand = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            isStringPulled = false;
            leftHand = null;
        }
    }

    private void CreateArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, bowHandle.position, Quaternion.identity);
            currentArrow.transform.SetParent(bowHandle);
        }
    }

    private void ShootArrow()
    {
        if (currentArrow != null)
        {
            currentArrow.transform.SetParent(null);
            Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
            float pullDistance = Mathf.Abs(bowHandle.position.z - originalHandlePosition.z);
            rb.AddForce(transform.forward * pullDistance * pullStrengthMultiplier);
            currentArrow = null;
        }
    }

    private void ResetString()
    {
        bowHandle.position = originalHandlePosition;
    }
}
