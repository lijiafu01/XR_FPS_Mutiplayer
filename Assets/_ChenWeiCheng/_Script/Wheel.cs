using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    bool canCollider = true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dev_is collider 1");
        if (other.tag == "controller" && canCollider)
        {
            Debug.Log("dev_is collider 2");
            // Đặt đối tượng là con của controller để giữ vị trí tương đối
            transform.SetParent(other.transform);
            // Đặt lại vị trí của đối tượng tới vị trí ban đầu trong hệ toạ độ cục bộ của controller
            transform.localPosition = Vector3.zero;
            canCollider = false;
        }
    }
}
