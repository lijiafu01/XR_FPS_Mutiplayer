using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    Pistol,
    Grenade
}
public class WeaponManager : MonoBehaviour
{

    [SerializeField]  private WeaponType _currentWeapon;
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
    public WeaponType CurrentWeapon
    {
        get { return _currentWeapon; }
        private set { _currentWeapon = value; }
    }
}
