using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWheel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "controller")
        {
            transform.position = other.transform.position;
        }
    }
}
