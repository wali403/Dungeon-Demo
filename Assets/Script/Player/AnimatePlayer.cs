using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        SetIdleAnimationParameters();
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent weaponEvent, AimWeaponEventArgs weaponEventArgs)
    {
        InitializeAimAnimationParameters();

        SetAimWeaponAnimationParameters(weaponEventArgs.aimDirection);
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        SetMovementAnimationParameters();
    }

    private void InitializeAimAnimationParameters()
    {
        player.animator.SetBool(Settings.aimRight, false);
        player.animator.SetBool(Settings.aimLeft, false);
    }

    private void SetIdleAnimationParameters()
    {
        player.animator.SetBool(Settings.isRun, false);
        player.animator.SetBool(Settings.isIdle, true);
    }

    private void SetAimWeaponAnimationParameters(AimDirection aimDirection)
    {
        switch (aimDirection)
        {
            case AimDirection.right:
                player.animator.SetBool(Settings.aimRight, true);
                break;
            case AimDirection.left:
                player.animator.SetBool(Settings.aimLeft, true);
                break;
        }
    }

    private void SetMovementAnimationParameters()
    {
        player.animator.SetBool(Settings.isRun, true);
        player.animator.SetBool(Settings.isIdle, false);
    }
}
