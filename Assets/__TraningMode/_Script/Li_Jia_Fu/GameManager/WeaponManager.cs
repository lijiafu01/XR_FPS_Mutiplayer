using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Weapon
{
    Pistol,
    Grenade
}
public class WeaponManager : MonoBehaviour
{

    [SerializeField]  private Weapon _currentWeapon;
    public WeaponBehaviour WeaponBehaviour;
    public static WeaponManager Instance { get; private set; }
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
    public Weapon CurrentWeapon
    {
        get { return _currentWeapon; }
        private set { _currentWeapon = value; }
    }
}
