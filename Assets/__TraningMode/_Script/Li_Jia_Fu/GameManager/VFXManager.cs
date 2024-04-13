using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject pistolMuzzleFlashVFX;
/*    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private GameObject m_Prefab;*/

    public static VFXManager Instance { get; private set; }
    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PistolMuzzelVFX()
    {

    }
}
