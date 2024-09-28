/*using System.Collections;
using UnityEngine;

namespace BeyondLimitsStudios
{
    namespace VRInteractables
    {
        public class BowAutoShoot : MonoBehaviour
        {
            [SerializeField]
            private bool on = false;
            [SerializeField]
            private bool onlyPullTheBowstring = false;
            [SerializeField]
            private float pullDistance = 0.5f;
            [SerializeField]
            private float pullSpeed = 3f;
            [SerializeField]
            private float waitTime = 1f;
            [SerializeField]
            private float resetTime = 1f;

            [SerializeField]
            private EnergyBow bow;
            [SerializeField]
            private Transform bowstringHandle;

            private void Awake()
            {
                if (!on)
                    return;

                if (onlyPullTheBowstring)
                {
                    bow.OnHandleGrabbed();
                    bowstringHandle.transform.position -= bowstringHandle.transform.forward * pullDistance;
                }
                else
                {
                    StartCoroutine(ShotCycle());
                }
            }

            private IEnumerator ShotCycle()
            {
                while (true)
                {
                    bow.OnHandleGrabbed();

                    float totalPullDist = 0f;

                    while (totalPullDist < pullDistance)
                    {
                        float distToPull = pullDistance * Time.deltaTime * pullSpeed;
                        bowstringHandle.transform.position -= bowstringHandle.transform.forward * distToPull;
                        totalPullDist += distToPull;
                        yield return null;
                    }

                    yield return new WaitForSeconds(waitTime);

                    bow.OnHandleReleased();

                    yield return new WaitForSeconds(resetTime);

                    yield return null;
                }
            }
        }
    }
}*/