using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CharacterBehaviour
{
    [Tooltip("Character Animator.")]
    [SerializeField]
    private Animator characterAnimator;
   
    private WeaponBehaviour equippedWeapon;
    private WeaponAttachmentManagerBehaviour weaponAttachmentManager;

    protected override void Update()
    {
        if (InputManager.Instance.GetTriggerPressed())
        {
            Debug.Log("Người chơi tấn công");
            Fire();
        }
    }
    private void Fire()
    {
        equippedWeapon.Fire();
    }
}
