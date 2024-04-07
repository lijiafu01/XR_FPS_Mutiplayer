// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;


public abstract class WeaponBehaviour : MonoBehaviour
{
    protected float fireRate = 2f; // Súng có thể bắn mỗi 2 giây một lần.
    protected float nextFireTime = 0f;
    #region UNITY

    /// <summary>
    /// Awake.
    /// </summary>
    protected virtual void Awake() { }

    /// <summary>
    /// Start.
    /// </summary>
    protected virtual void Start() { }

    /// <summary>
    /// Update.
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// Late Update.
    /// </summary>
    protected virtual void LateUpdate() { }

    #endregion
    #region METHODS 
    public abstract void Fire();
    public abstract void Reload();
    public abstract void FillAmmunition(int amount);
    #endregion
}
