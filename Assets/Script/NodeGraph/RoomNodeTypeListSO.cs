using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeTypeList", menuName = "ScriptableObjects/Dungeon/Room Node Type List")]
public class RoomNodeTypeListSO : ScriptableObject
{
    //在这个特定的用例下比enum更灵活
    //ToolTip:在编译器中,当鼠标悬浮在该字段上时,会弹出显示的指定信息
    #region Header ROOM NODE TYPE LIST
    [Space(10)]
    [Header("ROOM NODE TYPE LIST")]
    #endregion
    #region ToolTip
    [Tooltip("这个列表应当填充所有游戏中的房间类型,用它来代替enum")]
    #endregion
    public List<RoomNodeTypeSO> list;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
#endif
    #endregion
}
