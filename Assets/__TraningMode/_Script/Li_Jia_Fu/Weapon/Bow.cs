using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
using TraningMode;
public class Bow : WeaponBehaviour
{
    [SerializeField] private Transform bowHandle;       // Điểm nắm giữa dây cung
    [SerializeField] private GameObject arrowPrefab;    // Prefab của mũi tên
    [SerializeField] private LineRenderer bowString;    // LineRenderer để vẽ dây cung
    [SerializeField] private float maxPullDistance = -0.5f; // Khoảng cách kéo tối đa, giá trị âm
    [SerializeField] private float pullStrengthMultiplier = 1000f; // Nhân số để tính lực bắn dựa trên độ kéo
    private GameObject currentArrow;
    private bool isStringPulled = false;
    private Transform leftHand;                        // Vị trí của controller tay trái
    public Transform attackTransform; // Đảm bảo rằng bạn đã gán đúng Transform này trong Unity Editor
    public Transform bowstringCenter;
    protected override void Start()
    {

        ResetString();
    }
    public override void Fire()
    {
        if (leftHand != null && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            if (!isStringPulled)
            {
                CreateArrow();
                isStringPulled = true;
            }

            // Hướng kéo tuyến tính từ bowStringCenter đến attackTransform
            Vector3 pullDirection = (attackTransform.position - bowstringCenter.position).normalized;

            // Khoảng cách từ tay đến bowStringCenter
            float handDistance = Vector3.Distance(leftHand.position, bowstringCenter.position);

            // Đảo ngược hướng kéo để phù hợp với cách thực tế kéo dây cung
            // Giảm khoảng cách kéo nếu vượt quá giới hạn cho phép
            float pullDistance = Mathf.Max(0, Mathf.Min(handDistance, -maxPullDistance));

            // Cập nhật vị trí của bowHandle dọc theo hướng kéo đúng
            bowHandle.position = bowstringCenter.position - pullDirection * pullDistance;

            // Cập nhật hướng của mũi tên để luôn hướng theo attackTransform
            if (currentArrow != null)
            {
                currentArrow.transform.rotation = Quaternion.LookRotation(attackTransform.forward);
            }
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
        }
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void FillAmmunition(int amount)
    {
        throw new System.NotImplementedException();
    }
    private void CreateArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = ObjectPoolManager.Instance.SpawnFromPool("arrow", bowHandle.position, Quaternion.identity);
            //currentArrow = Instantiate(arrowPrefab, bowHandle.position, Quaternion.identity);
            currentArrow.transform.SetParent(bowHandle);
        }
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
    private void ShootArrow()
    {
        if (currentArrow != null)
        {
            currentArrow.transform.SetParent(null);
            Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
            float pullDistance = Mathf.Abs(bowHandle.position.z - bowstringCenter.position.z);
            rb.AddForce(transform.forward * pullDistance * pullStrengthMultiplier);
            currentArrow = null;
            //Invoke("SetBowCollider", 0.05f);          
            
        }
    }
    /*private void SetBowCollider()
    {
        currentArrow.GetComponent<CapsuleCollider>().isTrigger = false;
        currentArrow = null;
    }*/
    private void ResetString()
    {
        bowHandle.position = bowstringCenter.position;
    }

    
}