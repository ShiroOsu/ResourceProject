using System;
using System.Collections;
using UnityEngine;

namespace Code.FogOfWar
{
    public class FogProjector : MonoBehaviour
    {
        [SerializeField] private Projector projector;
        [SerializeField] private Material projectorMaterial;
        [SerializeField] private float blendSpeed;
        [SerializeField] private int textureScale;
        [SerializeField] private RenderTexture fogTexture;
        private RenderTexture m_PreviousTexture;
        private RenderTexture m_CurrentTexture;
        private float m_BlendAmount;
        private static readonly int PrevTexture = Shader.PropertyToID("_PrevTexture");
        private static readonly int CurrTexture = Shader.PropertyToID("_CurrTexture");
        private static readonly int Blend = Shader.PropertyToID("_Blend");

        private void Awake()
        {
            projector.enabled = true;
            m_PreviousTexture = GenerateTexture();
            m_CurrentTexture = GenerateTexture();

            projector.material = new Material(projectorMaterial);
            projector.material.SetTexture(PrevTexture, m_PreviousTexture);
            projector.material.SetTexture(CurrTexture, m_CurrentTexture);

            StartNewBlend();
        }

        private RenderTexture GenerateTexture()
        {
            var width = fogTexture.width * textureScale;
            var height = fogTexture.height * textureScale;
            
            var tex = new RenderTexture(width, height, 0, fogTexture.format)
            {
                filterMode  = FilterMode.Bilinear,
                antiAliasing = fogTexture.antiAliasing
            };

            return tex;
        }

        private void StartNewBlend()
        {
            StopCoroutine(BlendFog());
            m_BlendAmount = 0f;
            Graphics.Blit(m_CurrentTexture, m_PreviousTexture);
            Graphics.Blit(fogTexture, m_CurrentTexture);
            StartCoroutine(BlendFog());
        }

        private IEnumerator BlendFog()
        {
            while (m_BlendAmount < 1)
            {
                m_BlendAmount += Time.deltaTime * blendSpeed;
                projector.material.SetFloat(Blend, m_BlendAmount);
                yield return null;
            }
            
            StartNewBlend();
        }
    }
}