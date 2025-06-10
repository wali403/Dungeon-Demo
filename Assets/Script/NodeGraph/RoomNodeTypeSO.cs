using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "ScriptableObjects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    #region Header
    [Header("�÷��������Ƿ���ʾ��ͼ��༭����")]
    #endregion Header
    public bool displayInNodeGraphEditor = true;

    #region Header
    [Header("��������Ϊ����")]
    #endregion Header
    public bool isCorridor;

    #region Header
    [Header("��������Ϊ�ϱ�����")]
    #endregion Header
    public bool isCorridorNS;

    #region Header
    [Header("��������Ϊ��������")]
    #endregion Header
    public bool isCorridorEW;

    #region Header
    [Header("��������Ϊ���")]
    #endregion Header
    public bool isEntrance;

    #region Header
    [Header("��������ΪBoss��")]
    #endregion Header
    public bool isBossRoom;

    #region Header
    [Header("��������Ϊ��(δ����)")]
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
