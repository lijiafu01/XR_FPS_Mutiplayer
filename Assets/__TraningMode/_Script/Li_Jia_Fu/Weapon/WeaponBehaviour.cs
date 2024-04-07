// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;


public abstract class WeaponBehaviour : MonoBehaviour
{
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
    /// <summary>
    /// Reloads the weapon.
    /// </summary>
    public abstract void Reload();

    /// <summary>
    /// Fills the character's equipped weapon's ammunition by a certain amount, or fully if set to -1.
    /// </summary>
    public abstract void FillAmmunition(int amount);

    
    #endregion
}
