using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraningMode
{
    public class PlayerAttack : AttackBehaviour
    {
        private WeaponBehaviour equippedWeapon;
        protected override void Start()
        {
            equippedWeapon = PlayerController.Instance.PlayerWeapon.WeaponBehaviour;
        }

        protected override void Update()
        {
            Attack();
            Reload();
        }
        protected override void Attack()
        {
            if (equippedWeapon == null) return;
            equippedWeapon.Fire();
        }

        protected override void Reload()
        {
            if (equippedWeapon == null) return;
            equippedWeapon.Reload();
        }
    }
}

