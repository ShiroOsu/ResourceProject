using UnityEngine;
using UnityEngine.VFX;

public class FirstVFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect m_VisualEffect;

    [SerializeField, Range(0f, 5f)]
    private float m_SpawnRate = 0;

    private void Update()
    {
        m_VisualEffect.SetFloat("Intensity", m_SpawnRate);
    }
}