using UnityEngine;
using UnityEngine.UI;

namespace BaseGame.Scripts.Gameplay.Health
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            if (_slider == null)
                _slider = GetComponentInChildren<Slider>();

            gameObject.SetActive(false);
        }

        public void Setup(float maxHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
            gameObject.SetActive(true);
        }

        public void OnHealthChanged(float current, float max)
        {
            _slider.value = current;
        }

        public void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}