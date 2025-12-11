using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AimWeaponEvent : MonoBehaviour
{
    public event Action<AimWeaponEvent, AimWeaponEventArgs> OnWeaponAim;

    public void CallAimWeaponEvent(AimDirection aimDirection, float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector)
    {
        // ?表示,如果事件中没有订阅任何事件,那么就不执行这条代码,不会报错
        OnWeaponAim?.Invoke(this, new AimWeaponEventArgs()
        {
            aimDirection = aimDirection,
            aimAngle = aimAngle,
            weaponAimAngle = weaponAimAngle,
            weaponAimDirectionVector = weaponAimDirectionVector
        });
    }
}

public class AimWeaponEventArgs : EventArgs
{
    public AimDirection aimDirection;//enum的方向,瞄准方向
    public float aimAngle;//玩家和瞄准点的角度
    public float weaponAimAngle;//武器和瞄准点的角度
    public Vector3 weaponAimDirectionVector;//武器和瞄准点之间的向量
}
