using UnityEngine;
using TraningMode;
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
    public abstract void Reload();
    public abstract void FillAmmunition(int amount);
    #endregion
}
