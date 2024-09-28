/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BeyondLimitsStudios
{
    namespace VRInteractables
    {
        public class EnergyBow : MonoBehaviour
        {
            [SerializeField]
            private XRGrabInteractable handleGrabInteractable;
            [SerializeField]
            private Transform bowstringHandle;
            [SerializeField]
            private Transform arrowRest;
            [SerializeField]
            private Transform bowstringCenter;
            [SerializeField]
            private EnergyArrow arrow;

            [SerializeField]
            private GameObject arrowPrefab;

            [SerializeField]
            private List<Collider> bowColliders = new List<Collider>();

            [SerializeField]
            private List<Collider> bowstringHandleColliders = new List<Collider>();

            [SerializeField]
            private BowConfig bowConfig;

            [SerializeField]
            private ArrowConfig arrowConfig;

            private bool handleHeld = false;

            void Awake()
            {
                if (bowstringHandle == null)
                {
                    Debug.LogError($"BowstringHandle not assigned!");
                    this.enabled = false;
                }

                if (arrowRest == null)
                {
                    Debug.LogError($"ArrowRest not assigned!");
                    this.enabled = false;
                }

                if (arrowPrefab == null)
                {
                    Debug.LogError($"ArrowPrefab not assigned!");
                    this.enabled = false;
                }

                if (arrowPrefab.GetComponent<EnergyArrow>() == null)
                {
                    Debug.LogError($"ArrowPrefab need to have EnergyArrow component!");
                    this.enabled = false;
                }

                foreach (var col1 in bowColliders)
                {
                    foreach (var col2 in bowstringHandleColliders)
                    {
                        if (col1 != col2)
                            Physics.IgnoreCollision(col1, col2, true);
                    }
                }

                if (arrow == null)
                {
                    SpawnArrowPrefab();
                }

            }

            // Update is called once per frame
            void Update()
            {
                if (handleHeld)
                {
                    bowstringHandle.transform.position = bowConfig.alwaysAimStraight ? ClosestPointOnLine(arrowRest.position, bowstringCenter.position, handleGrabInteractable.transform.position) : handleGrabInteractable.transform.position;

                    if (arrow.IsArrowEnabled())
                    {
                        if (ShouldDisableArrow())
                            arrow.DisableArrow();
                    }
                    else
                    {
                        if (ShouldEnableArrow())
                            arrow.EnableArrow();
                    }

                    if (!IsPositionValid())
                        ForceRelease();
                }
                else
                {
                    bowstringHandle.transform.position = Vector3.Lerp(bowstringHandle.transform.position, bowstringCenter.transform.position, Time.deltaTime * bowConfig.resetSpeed);
                    bowstringHandle.transform.rotation = Quaternion.Lerp(bowstringHandle.transform.rotation, bowstringCenter.transform.rotation, Time.deltaTime * bowConfig.resetSpeed);
                    handleGrabInteractable.transform.position = bowstringHandle.transform.position;
                    handleGrabInteractable.transform.rotation = bowstringHandle.transform.rotation;
                }

                arrow.transform.position = bowstringHandle.transform.position;
                arrow.transform.LookAt(arrowRest);
            }

            private Vector3 ClosestPointOnLine(Vector3 point1, Vector3 point2, Vector3 point)
            {
                return point1 + Vector3.Project(point - point1, point2 - point1);
            }

            private void SpawnArrowPrefab()
            {
                GameObject go = Instantiate(arrowPrefab);

                go.transform.parent = this.transform;

                arrow = go.GetComponent<EnergyArrow>();

                arrow.SetExcludedColliders(bowColliders);
                arrow.SetArrowConfig(arrowConfig);
            }

            private void ForceRelease()
            {
                //handleHeld = false;

                //arrow.DisableArrow();

                // some grabbable code here
                // handleGrabInteractable.interactionManager.CancelInteractableSelection(handleGrabInteractable);
                handleGrabInteractable.ForceDeselect();
            }

            public void OnHandleGrabbed()
            {
                handleHeld = true;
            }

            public void OnHandleReleased()
            {
                handleHeld = false;

                if (ShouldShoot())
                {
                    Shoot();
                    SpawnArrowPrefab();
                }
                else
                {
                    arrow.DisableArrow();
                }
            }

            private void Shoot()
            {
                arrow.Shoot();

                arrow = null;
            }

            private bool IsPositionValid()
            {
                if (Vector3.SqrMagnitude(bowstringHandle.position - bowstringCenter.position) > bowConfig.maxDistance * bowConfig.maxDistance)
                    return false;

                if (Vector3.Angle(bowstringHandle.position - arrowRest.position, bowstringCenter.position - arrowRest.position) > bowConfig.maxAngle)
                    return false;

                if (Vector3.SqrMagnitude(bowstringHandle.position - handleGrabInteractable.transform.position) > bowConfig.maxHandToHandleDistance * bowConfig.maxHandToHandleDistance)
                    return false;

                return true;
            }

            private bool ShouldShoot()
            {
                float handleToCenter = Vector3.SqrMagnitude(bowstringHandle.position - bowstringCenter.position);
                float handleToRest = Vector3.SqrMagnitude(bowstringHandle.position - arrowRest.position);
                float centerToRest = Vector3.SqrMagnitude(bowstringCenter.position - arrowRest.position);

                if (handleToRest > centerToRest && handleToCenter > bowConfig.minDistanceToShoot * bowConfig.minDistanceToShoot)
                    return true;

                return false;
            }

            private bool ShouldEnableArrow()
            {
                float handleToCenter = Vector3.SqrMagnitude(bowstringHandle.position - bowstringCenter.position);
                float handleToRest = Vector3.SqrMagnitude(bowstringHandle.position - arrowRest.position);
                float centerToRest = Vector3.SqrMagnitude(bowstringCenter.position - arrowRest.position);

                float angle = Vector3.Angle(bowstringHandle.position - bowstringCenter.position, arrowRest.position - bowstringCenter.position);

                if (angle >= 90f && handleToRest > centerToRest && handleToCenter > bowConfig.minDistanceToShoot * bowConfig.minDistanceToShoot)
                    return true;

                return false;
            }

            private bool ShouldDisableArrow()
            {
                float handleToCenter = Vector3.SqrMagnitude(bowstringHandle.position - bowstringCenter.position);
                float handleToRest = Vector3.SqrMagnitude(bowstringHandle.position - arrowRest.position);
                float centerToRest = Vector3.SqrMagnitude(bowstringCenter.position - arrowRest.position);

                float angle = Vector3.Angle(bowstringHandle.position - bowstringCenter.position, arrowRest.position - bowstringCenter.position);

                if (angle < 80f || handleToRest < centerToRest || handleToCenter < (bowConfig.minDistanceToShoot * 0.9f) * (bowConfig.minDistanceToShoot * 0.9f))
                    return true;

                return false;
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(EnergyBow)), CanEditMultipleObjects]
        class EnergyBowEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var energyBow = (EnergyBow)target;
                if (energyBow == null) return;

                //if (GUILayout.Button("Start Holding"))
                //    energyBow.OnHandleGrabbed();

                //if (GUILayout.Button("Stop Holding"))
                //    energyBow.OnHandleReleased();

                DrawDefaultInspector();
            }
        }
#endif
    }
}*/