using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimWeaponEvent))]
[DisallowMultipleComponent]
public class AimWeapon : MonoBehaviour
{
    #region Tooltip
    [Tooltip("用子物体武器旋转点的transform进行填充")]
    #endregion
    [SerializeField] private Transform weaponRotationPointTransform;//武器瞄准点的位置

    private AimWeaponEvent aimWeaponEvent;

    private void Awake()
    {
        aimWeaponEvent = GetComponent<AimWeaponEvent>();

    }

    //组件激活时自动调用
    private void OnEnable()
    {
        //订阅事件
        aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }

    //组件取消激活时自动调用
    private void OnDisable()
    {
        //取消订阅事件
        aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }

    //需要有和订阅事件相同的参数
    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        Aim(aimWeaponEventArgs.aimDirection, aimWeaponEventArgs.aimAngle);
    }

    private void Aim(AimDirection aimDirection, float aimAngle)
    {
        weaponRotationPointTransform.eulerAngles = new Vector3(0f, 0f, aimAngle);//分别在x轴, y轴, z轴上的偏移

        switch (aimDirection)
        {
            case AimDirection.left:
                weaponRotationPointTransform.localScale = new Vector3(-1f, -1f, 0f);
                break;

            case AimDirection.right:
                weaponRotationPointTransform.localScale = new Vector3(1f, 1f, 0f);
                break;
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponRotationPointTransform), weaponRotationPointTransform);
    }
#endif
    #endregion
}
