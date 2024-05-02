/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

namespace BeyondLimitsStudios
{
    namespace VRInteractables
    {
        public class EnergyArrow : MonoBehaviour
        {
            //[SerializeField]
            //private float length;
            //[SerializeField] 
            //private float width;
            //[SerializeField]
            //private float enablingSpeed = 1f;
            //[SerializeField] 
            //private float speed = 5f;

            // [SerializeField]
            private ArrowConfig arrowConfig;

            [SerializeField]
            private LineRenderer lineRenderer;

            [SerializeField]
            private AudioSource audioSource;

            [SerializeField]
            private GameObject particlePrefab;

            private bool arrowEnabled;
            // private bool arrowShot;

            private List<Collider> excludedColliders = new List<Collider>();

            private Color color;

            private void Setup()
            {
                this.enabled = false;

                if(lineRenderer == null)
                    lineRenderer = GetComponentInChildren<LineRenderer>();

                if (lineRenderer == null)
                {
                    Debug.LogError($"LineRenderer not assigned!");
                    this.enabled = false;
                }

                lineRenderer.startWidth = arrowConfig.width;
                lineRenderer.endWidth = arrowConfig.width;

                lineRenderer.SetPosition(1, Vector3.zero);

                // lineRenderer.material.SetColor("_EmissionColor", arrowConfig.color);

                color = lineRenderer.material.GetColor("_EmissionColor");
            }

            private void Update()
            {
                // if (arrowShot)
                ProcessMovement();
                HitCheck();
            }

            public void SetExcludedColliders(List<Collider> colls)
            {
                excludedColliders.Clear();

                foreach (var col in colls)
                {
                    excludedColliders.Add(col);
                }
            }

            public void SetArrowConfig(ArrowConfig config)
            {
                arrowConfig = config;

                Setup();
            }

            public void ProcessMovement()
            {
                this.transform.position += this.transform.forward * Time.deltaTime * arrowConfig.speed;
            }

            public void HitCheck()
            {
                Ray ray = new Ray();
                ray.origin = arrowConfig.accurateCollisionCheck ? this.transform.position - this.transform.forward * arrowConfig.speed * Time.deltaTime : this.transform.position;
                ray.direction = this.transform.forward;

                float rayLength = arrowConfig.accurateCollisionCheck ? arrowConfig.length + arrowConfig.speed * Time.deltaTime : arrowConfig.length;

                RaycastHit[] hits = Physics.RaycastAll(ray, rayLength, arrowConfig.collisionLayer, QueryTriggerInteraction.Ignore);

                foreach (var hit in hits)
                {
                    if (excludedColliders.Contains(hit.collider))
                        continue;

                    IDamageable arrowTarget = hit.collider.GetComponentInParent<IDamageable>();

                    if (arrowTarget != null)
                    {
                        arrowTarget.ApplyDamage(new DamageData(arrowConfig.damage));
                    }

                    if (arrowConfig.applyForceToRBs)
                    {
                        // Rigidbody targetRb = hit.collider.GetComponentInParent<Rigidbody>();
                        Rigidbody targetRb = hit.collider.attachedRigidbody;

                        if (targetRb != null)
                        {
                            targetRb.AddForceAtPosition(ray.direction.normalized * arrowConfig.speed * arrowConfig.impactForce, hit.point);
                        }
                    }

                    if (arrowConfig.spawnParticlesOnCollision)
                    {
                        GameObject particles = Instantiate(particlePrefab);
                        particles.transform.position = hit.point;
                        particles.transform.up = hit.normal;

                        float h, s, v;
                        Color.RGBToHSV(color, out h, out s, out v);
                        s = 0.25f;
                        particles.GetComponent<VisualEffect>().SetVector4("Color", Color.HSVToRGB(h, s, v, true));
                    }

                    DestroyArrow();

                    break;
                }
            }

            public void DestroyArrow()
            {
                Destroy(this.gameObject);
            }

            public void Shoot()
            {
                // arrowShot = true;
                this.enabled = true;
                this.transform.parent = null;
                Destroy(this.gameObject, arrowConfig.timeToDestroy);
            }

            public void EnableArrow()
            {
                arrowEnabled = true;
                StopAllCoroutines();
                StartCoroutine(EnableArrowCoroutine());
                StartCoroutine(EnableAudioCoroutine());
            }

            public void DisableArrow()
            {
                arrowEnabled = false;
                StopAllCoroutines();
                StartCoroutine(DisableArrowCoroutine());
                StartCoroutine(DisableAudioCoroutine());
            }

            public bool IsArrowEnabled()
            {
                return arrowEnabled;
            }


            private IEnumerator EnableAudioCoroutine()
            {
                if (arrowConfig.volume <= 0)
                    yield break;

                float targetVolume = arrowConfig.volume;

                audioSource.volume = 0f;

                audioSource.Play();

                while (audioSource.volume < targetVolume * 0.95f)
                {
                    audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * arrowConfig.enablingAudioSpeed);
                    yield return null;
                }

                audioSource.volume = targetVolume;
            }

            private IEnumerator DisableAudioCoroutine()
            {
                if (arrowConfig.volume <= 0)
                    yield break;

                float targetVolume = arrowConfig.volume;

                while (audioSource.volume > 0.05f)
                {
                    audioSource.volume = Mathf.Lerp(audioSource.volume, 0, Time.deltaTime * arrowConfig.enablingAudioSpeed);
                    yield return null;
                }

                audioSource.Stop();

                audioSource.volume = arrowConfig.volume;
            }

            private IEnumerator EnableArrowCoroutine()
            {
                Vector3 target = new Vector3(0f, 0f, arrowConfig.length);

                while (Vector3.SqrMagnitude(lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)) < arrowConfig.length * arrowConfig.length)
                {
                    lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), target, Time.deltaTime * arrowConfig.enablingSpeed));
                    yield return null;
                }
            }

            private IEnumerator DisableArrowCoroutine()
            {
                while (Vector3.SqrMagnitude(lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)) > 0f)
                {
                    lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), Vector3.zero, Time.deltaTime * arrowConfig.enablingSpeed));
                    yield return null;
                }
            }
        }
    }
}*/