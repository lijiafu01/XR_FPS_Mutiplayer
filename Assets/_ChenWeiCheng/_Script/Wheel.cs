using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dev_is collider 1");
        if (other.tag == "controller")
        {
            Debug.Log("dev_is collider 2");
            transform.position = other.transform.position;
        }
    }
}
