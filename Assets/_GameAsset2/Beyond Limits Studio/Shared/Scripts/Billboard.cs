using UnityEngine;

namespace BeyondLimitsStudios
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Transform cam;

        // Update is called once per frame
        void LateUpdate()
        {
            if (cam == null)
                cam = FindObjectOfType<Camera>().transform;

            if (cam == null)
                this.enabled = false;

            this.transform.LookAt(this.transform.position + cam.forward);
        }
    }
}