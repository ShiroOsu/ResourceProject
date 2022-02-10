using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarImage;
        [SerializeField] private Gradient gradient;

        private void Update()
        {
            healthBarImage.color = gradient.Evaluate(healthBarImage.fillAmount);
        }
    }
}
