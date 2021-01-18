using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_gameObject = null;

    private void Start()
    {
        m_gameObject = null;

        DontDestroyOnLoad(this);
    }
}