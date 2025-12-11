using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(Health))]//健康组件
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(SortingGroup))]//用来在一个有多个子物体的物体内部渲染排序
[RequireComponent(typeof(SpriteRenderer))]//精灵渲染器
[RequireComponent(typeof(Animator))]//动画机
[RequireComponent(typeof(BoxCollider2D))]//碰撞检测,让玩家与地牢碰撞方块正常碰撞
[RequireComponent(typeof(PolygonCollider2D))]//多边形碰撞器用于检测事件,如被敌人子弹击中
[RequireComponent(typeof(Rigidbody2D))]//用于与2D物理互动

[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        health = GetComponent<Health>();
        idleEvent = GetComponent<IdleEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
}
