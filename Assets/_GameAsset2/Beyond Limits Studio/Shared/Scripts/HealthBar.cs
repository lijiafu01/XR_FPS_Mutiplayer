using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BeyondLimitsStudios
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Image fill;
        [SerializeField]
        private Gradient gradient;
        [SerializeField]
        private float width;
        [SerializeField]
        private float height;

        [SerializeField]
        private float animationSpeed = 5f;

        private float targetHealth;

        private void Awake()
        {
            RectTransform rectTransform = (RectTransform)this.transform;

            rectTransform.sizeDelta = new Vector2(width, height);
        }

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            targetHealth = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);
        }

        public void SetHealth(float health)
        {
            // slider.value = health;
            targetHealth = health;

            StopAllCoroutines();
            StartCoroutine(AnimateHealthBar());
        }

        private IEnumerator AnimateHealthBar()
        {
            float speed = Mathf.Abs(slider.value - targetHealth);

            while (slider.value != targetHealth)
            {
                float maxDelta = Time.deltaTime * animationSpeed * speed;
                slider.value = Mathf.MoveTowards(slider.value, targetHealth, maxDelta);
                fill.color = gradient.Evaluate(slider.normalizedValue);
                yield return null;
            }
        }
    }
}