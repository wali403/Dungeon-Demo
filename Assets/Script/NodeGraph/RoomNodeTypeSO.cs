using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "ScriptableObjects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    #region Header
    [Header("该房间类型是否显示在图像编辑器中")]
    #endregion Header
    public bool displayInNodeGraphEditor = true;

    #region Header
    [Header("房间类型为走廊")]
    #endregion Header
    public bool isCorridor;

    #region Header
    [Header("房间类型为南北走廊")]
    #endregion Header
    public bool isCorridorNS;

    #region Header
    [Header("房间类型为东西走廊")]
    #endregion Header
    public bool isCorridorEW;

    #region Header
    [Header("房间类型为入口")]
    #endregion Header
    public bool isEntrance;

    #region Header
    [Header("房间类型为Boss房")]
    #endregion Header
    public bool isBossRoom;

    #region Header
    [Header("房间类型为空(未设置)")]
    #endregion Header
    public bool isNone;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
    }
#endif
    #endregion
}
