using UnityEngine;

public class VRController : MonoBehaviour
{
    public GameObject rightHandController;
    private void Update()
    {
        transform.position = rightHandController.transform.position;
        transform.rotation = rightHandController.transform.rotation;
    }
}
