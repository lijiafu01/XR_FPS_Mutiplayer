using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;

[System.Serializable]
public class WeaponData
{
    public WeaponType weaponType;  // Enum cho loại vũ khí
    public GameObject weaponObject;  // GameObject của vũ khí
}
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private List<WeaponData> playerWeapons = new List<WeaponData>();
    public WeaponBehaviour WeaponBehaviour;
    private void Start()
    {
        SetPlayerWeapon();
    }
   
    private void SetPlayerWeapon()
    {

        WeaponType currentWeapon = WeaponManager.Instance.CurrentWeapon;
        foreach (WeaponData weaponData in playerWeapons)
        {
            // Kích hoạt GameObject nếu loại vũ khí khớp
            if (weaponData.weaponType == currentWeapon)
            {
                weaponData.weaponObject.SetActive(true);

                WeaponBehaviour = weaponData.weaponObject.GetComponent<WeaponBehaviour>();
                Debug.Log("Activated weapon: " + weaponData.weaponObject.name);
            }
            else
            {
                weaponData.weaponObject.SetActive(false);
            }
        }
    }
}
