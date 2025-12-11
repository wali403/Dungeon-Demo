using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{

    #region UNITY

    public const float pixelsPerUnit = 16f;
    public const float tileSizePixels = 16f;

    #endregion

    #region DUNGEON BUILD SETTINGS

    //最大房间图重建次数
    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    //最大地牢建造次数
    public const int maxDungeonBuildAttempts = 10;

    #endregion

    #region ROOM SETTINGS

    public const float fadeInTime = 0.5f;//淡入时间,需要多久来显示房间
    public const int maxChildCorridors = 3;//单个房间可生成的最大子走廊数（虽然允许设置为3，
                                           //但是不推荐该最大值,可能会使地牢生成失败,因为这会使房间更难合理衔接）
    #endregion

    #region ANIMATOR PARAMETERS

    //对于动画机中的属性,创建对应字符串的哈希值,对比字符串来说,不容易出错且更快
    public static int aimRight = Animator.StringToHash("aimRight");
    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isRun = Animator.StringToHash("isRun");
    public static float baseSpeedForPlayerAnimations = 8f;

    #endregion

    #region GAMEOBJECT TAGS

    public const string playerTag = "Player";
    public const string playerWeapon = "playerWeapon";

    #endregion
}
