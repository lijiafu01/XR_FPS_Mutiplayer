using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBehaviour
{
    [SerializeField]
    private WeaponBehaviour equippedWeapon;
    protected override void Start()
    {
    }
    protected override void Update()
    {
        //CheckPlayerInput();
    }
    /*protected override void CheckPlayerInput()
    {
        if (InputManager.Instance.GetTriggerPressed())
        {
            Debug.Log("dev_Người chơi tấn công");
            Attack();
        }
        if (InputManager.Instance.GetRightGripReleased())
        {
            Debug.Log("dev_Người chơi ném lựu đạn");


        }
        if (InputManager.Instance.IsRightGripPressed())
        {
            Debug.Log("dev_nhan nut grip");
            Attack();
        }
    }*/
    protected override void Attack()
    {
        equippedWeapon.Fire();
    }
}
