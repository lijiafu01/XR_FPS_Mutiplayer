using UnityEngine;
using TraningMode;
public class VRController : MonoBehaviour
{
    public GameObject Controller;
    private void Update()
    {
        transform.position = Controller.transform.position;
        transform.rotation = Controller.transform.rotation;
    }
}
