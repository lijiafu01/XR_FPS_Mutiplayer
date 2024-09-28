using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace BeyondLimitsStudios
{
    public class DisableParticles : MonoBehaviour
    {
        [SerializeField]
        private VisualEffect vfx;

        [SerializeField]
        private float timeToDisable = 0.1f;
        [SerializeField]
        private float timeToDestroy = 4.0f;

        // Start is called before the first frame update
        private void Awake()
        {
            if (vfx == null)
            {
                vfx = GetComponentInChildren<VisualEffect>();
            }

            if (vfx == null)
            {
                Debug.LogError("Couldn't fine VisualEffect on gameobject!");
                return;
            }

            if (timeToDestroy <= timeToDisable)
            {
                Debug.LogError("TimeToDestroy has to be greater than TimeToDisable!");
                return;
            }

            vfx.Play();

            StartCoroutine(TurnParticlesOff());
        }

        private IEnumerator TurnParticlesOff()
        {
            yield return null;
            yield return new WaitForSeconds(timeToDisable);

            vfx.Stop();

            Destroy(this.gameObject, timeToDestroy - timeToDisable);
        }
    }
}