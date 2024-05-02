using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public abstract class AttackBehaviour : MonoBehaviour
{
    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void LateUpdate() { }

    protected abstract void Attack();
    protected abstract void Reload();
}
