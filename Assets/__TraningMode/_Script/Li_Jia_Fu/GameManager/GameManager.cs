using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Weapon
{
    Pistol,
    Grenade
}
public class GameManager : MonoBehaviour
{
    public Weapon currentWeapon;
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
