using Unity.VisualScripting;
using UnityEngine;
using TraningMode;
public class Bowstring : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform bowstringTarget;

    [SerializeField]
    private float offset = 0.05f;

    void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponentInChildren<LineRenderer>();

        if (lineRenderer == null)
        {
            this.enabled = false;
            return;
        }

        if (bowstringTarget == null)
        {
            foreach (Transform child in lineRenderer.transform)
            {
                bowstringTarget = child.gameObject.GetComponentInChildren<Transform>();

                if (bowstringTarget != null)
                    break;
            }
        }

        if (bowstringTarget == null)
        {
            this.enabled = false;
            return;
        }
    }
   /* private void Start()
    {
        Vector3 newpos = new Vector3(bowstringTarget.position.x,bowstringTarget.position.y,bowstringTarget.position.z-0.3f);
        bowstringTarget.position = newpos;
    }*/

    void LateUpdate()
    {
        Vector3 centerLocalSpace = this.transform.InverseTransformPoint(bowstringTarget.transform.position);
        Vector3 dir = this.transform.InverseTransformDirection(bowstringTarget.transform.up);

        lineRenderer.SetPosition(1, centerLocalSpace + dir * offset);
        lineRenderer.SetPosition(2, centerLocalSpace);
        lineRenderer.SetPosition(3, centerLocalSpace - dir * offset);
    }

}