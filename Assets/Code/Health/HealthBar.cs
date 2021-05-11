using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image m_HealthBarImage;
    [SerializeField] private Gradient m_Gradient;

    private void Update()
    {
        m_HealthBarImage.color = m_Gradient.Evaluate(m_HealthBarImage.fillAmount);
    }
}
