using UnityEngine;

namespace BeyondLimitsStudios
{
    public class ApplyForceFromCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        [SerializeField]
        private float strength = 10f;

        [SerializeField]
        private LayerMask layerMask;

        private void Awake()
        {
            if(cam == null)
                cam = this.GetComponentInChildren<Camera>();

            if (cam == null)
                this.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, layerMask))
                {
                    if(hit.rigidbody != null)
                        hit.rigidbody.AddForceAtPosition(hit.normal * -strength, hit.point);
                    // hit.rigidbody.AddForceAtPosition(ray.direction * strength, hit.point);
                }
            }
        }
    }
}