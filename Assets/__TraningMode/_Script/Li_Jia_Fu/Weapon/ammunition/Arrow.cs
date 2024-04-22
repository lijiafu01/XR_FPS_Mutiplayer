using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;  // Reference to the Rigidbody component
    [SerializeField] private float penetrationDepth = 0.5f; // Configurable penetration depth

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        
    }

    void Update()
    {
        // Optional: handle arrow logic here
    }

    // This function is called when the arrow triggers with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the object triggered with has the tag "target"
        if (other.CompareTag("target"))
        {
            Debug.Log("dev co vam cham");
            rb.isKinematic = true; // Ensure the Rigidbody doesn't affect the trigger simulation
            // Calculate the new position for the arrow, simulating 'soft' penetration
            Vector3 contactPoint = transform.position; // Assuming the arrow's tip is at its transform position
            Vector3 contactNormal = transform.forward; // Assuming the arrow's forward direction is the direction of entry

            // Adjust the position so the arrow appears to stick into the target
            // Move it slightly back from the contact point by the penetration depth along the contact normal
            transform.position = contactPoint + contactNormal * penetrationDepth;

            // Set the forward direction of the arrow to align with the contact normal
            transform.forward = contactNormal;

            // Parent the arrow to the target object to make it move and rotate with the target
            transform.SetParent(other.transform);
        }
    }
}
