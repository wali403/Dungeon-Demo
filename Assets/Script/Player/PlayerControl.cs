using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Tooltip
    [Tooltip("玩家武器瞄准点的位置")]
    #endregion
    [SerializeField] private Transform weaponShootPosition;

    #region Tooltip
    [Tooltip("储存如速度之类的移动细节")]
    #endregion
    [SerializeField] private MovementDetailsSO movementDetails;

    private Player player;
    private float moveSpeed;

    private void Awake()
    {
        player = GetComponent<Player>();

        moveSpeed = movementDetails.GetMoveSpeed();
    }

    private void Start()
    {
        SetPlayerAnimationSpeed();
    }

    private void SetPlayerAnimationSpeed()
    {
        player.animator.speed = moveSpeed / Settings.baseSpeedForPlayerAnimations;
    }

    private void Update()
    {
        MovementInput();

        WeaponInput();
    }

    private void MovementInput()
    {
        //右1 左-1 无0
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //上1 下-1 无0
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        //如果同时按下,需要水平和方向都*0.7,才能保证总方向只走了1的长度
        //0.49 + 0.49 约= 1 * 1
        if (horizontalMovement != 0 && verticalMovement != 0)
        {
            direction *= 0.7f;
        }

        if (direction != Vector2.zero)
        {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
        }
        else
        {
            player.idleEvent.CallIdleEvent();
        }
    }

    private void WeaponInput()
    {
        Vector3 weaponDirection;
        float weaponAngleDegrees, playerAngleDegrees;
        AimDirection playerAimDirection;

        //out会把输入的参数在函数中的改变传回出来
        AimWeaponInput(out weaponDirection, out weaponAngleDegrees, out playerAngleDegrees, out playerAimDirection);
    }

    private void AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees,
        out AimDirection playerAimDirection)
    {
        Vector3 mouseWorldPosition = HelperUtilities.GetMouseWorldPosition();

        weaponDirection = mouseWorldPosition - weaponShootPosition.position;

        Vector3 playerDirection = mouseWorldPosition - transform.position;

        weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);

        playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);

        playerAimDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);

        player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);

    }

    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }

#endif

    #endregion
}
