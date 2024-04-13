using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public Weapon weaponType;  // Enum cho loại vũ khí
    public GameObject weaponObject;  // GameObject của vũ khí
}

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<WeaponData> playerWeapons = new List<WeaponData>();

    private void Start()
    {
        SetPlayerWeapon();
    }

    private void SetPlayerWeapon()
    {
        Weapon currentWeapon = GameManager.Instance.currentWeapon; // Giả sử GameManager lưu trạng thái vũ khí hiện tại

        foreach (WeaponData weaponData in playerWeapons)
        {
            // Kích hoạt GameObject nếu loại vũ khí khớp
            if (weaponData.weaponType == currentWeapon)
            {
                weaponData.weaponObject.SetActive(true);
                Debug.Log("Activated weapon: " + weaponData.weaponObject.name);
            }
            else
            {
                weaponData.weaponObject.SetActive(false);
            }
        }
    }
}
