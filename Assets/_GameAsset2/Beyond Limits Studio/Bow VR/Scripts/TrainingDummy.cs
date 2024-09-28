/*using System.Collections;
using UnityEngine;

namespace BeyondLimitsStudios
{
    namespace VRInteractables
    {
        public class TrainingDummy : MonoBehaviour, IDamageable
        {
            [SerializeField]
            private float maxHealth = 100f;
            [SerializeField]
            private float currentHealth = 100f;

            [SerializeField]
            private float timeToHeal = 2f;

            [SerializeField]
            private HealthBar healthBar;

            private void Awake()
            {
                currentHealth = maxHealth;

                healthBar.SetMaxHealth(maxHealth);
            }

            public void ApplyDamage(DamageData damageData)
            {
                currentHealth -= damageData.Damage;

                if (currentHealth < 0f)
                    currentHealth = 0f;

                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                healthBar.SetHealth(currentHealth);

                StopAllCoroutines();
                StartCoroutine(Heal());
            }

            public IEnumerator Heal()
            {
                yield return new WaitForSeconds(timeToHeal);

                currentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
            }
        }
    }
}*/