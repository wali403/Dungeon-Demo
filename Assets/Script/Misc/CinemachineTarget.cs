using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineTarget : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;

    #region Tooltip
    [Tooltip("填充光标目标物体")]
    #endregion

    [SerializeField] private Transform cursorTarget;

    private void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void Start()
    {
        SetCinemachineTargetGroup();
    }

    private void SetCinemachineTargetGroup()
    {
        CinemachineTargetGroup.Target cinemachineGroupTarget_player = new CinemachineTargetGroup.Target
        {
            //创造对象时,直接为元素赋值
            weight = 1f,//权重
            radius = 2.5f,//让这个目标占的范围大一点，这里让玩家不容易出屏幕
            target = GameManager.Instance.GetPlayer().transform//指向的游戏对象
        };

        CinemachineTargetGroup.Target cinemachineGroupTarget_cursor = new CinemachineTargetGroup.Target
        {
            weight = 1f,//权重
            radius = 1f,
            target = cursorTarget//指向的游戏对象
        };

        CinemachineTargetGroup.Target[] cinemachineTargetArray = new CinemachineTargetGroup.Target[]
        {
            //创造数组时,直接填入元素
            cinemachineGroupTarget_player,//玩家
            cinemachineGroupTarget_cursor//鼠标
        };

        cinemachineTargetGroup.m_Targets = cinemachineTargetArray;//设置组件的目标数组为刚刚创建的数组
    }

    private void Update()
    {
        cursorTarget.position = HelperUtilities.GetMouseWorldPosition();
    }

}
