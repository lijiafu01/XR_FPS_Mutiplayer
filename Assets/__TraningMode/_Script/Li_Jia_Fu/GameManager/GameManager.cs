using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class GameManager : MonoBehaviour
{
    public WeaponType playerChooseWeapon;
    public bool isRun = false;
    public static GameManager Instance { get; private set; }
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
   
}
