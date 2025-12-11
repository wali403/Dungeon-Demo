using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "ScriptableObjects/Dungeon/DungeonLevel")]
public class DungeonLevelSO : ScriptableObject
{
    #region Header BASIC LEVEL DETAILS
    [Space(10)]
    [Header("BASIC LEVEL DETAILS")]
    #endregion Header BASIC LEVEL DETAILS

    #region Tooltip
    [Tooltip("the name for the level")]
    #endregion Tooltip

    public string levelName;

    #region Header ROOM TEMPLATES FOR LEVEL
    [Space(10)]
    [Header("ROOM TEMPLATES FOR LEVEL")]
    #endregion Header ROOM TEMPLATES FOR LEVEL

    #region Tooltip
    [Tooltip("你想要用来构建房间的房间模板列表,你需要确保所有房间连接图中的房间的房间类型" +
        "都存在对应类型的模板")]
    #endregion Tooltip

    public List<RoomTemplateSO> roomTemplateList;

    #region Header ROOM NODE GRAPHS FOR LEVEL
    [Space(10)]
    [Header("ROOM NODE GRAPHS FOR LEVEL")]
    #endregion Header ROOM NODE GRAPHS FOR LEVEL

    #region Tooltip
    [Tooltip("从关卡中随机选择房间连接图来填充这个列表")]
    #endregion Tooltip

    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        //检测各值是否为空
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList))
            return;
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
            return;

        //检测是否含有入口,东西走廊和南北走廊
        bool isEWCorridor = false;
        bool isNSCorridor = false;
        bool isEntrance = false;

        foreach(RoomTemplateSO roomTemplateSO in roomTemplateList)
        {
            if (roomTemplateSO == null)
                return;

            if (roomTemplateSO.roomNodeType.isCorridorEW)
                isEWCorridor = true;

            if (roomTemplateSO.roomNodeType.isCorridorNS)
                isNSCorridor = true;

            if (roomTemplateSO.roomNodeType.isEntrance)
                isEntrance = true;
        }

        if (isEWCorridor == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No E/W Corridor Room Type Specified");
        }

        if (isNSCorridor == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No N/S Corridor Room Type Specified");
        }

        if (isEntrance == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No Entrance Room Type Specified");
        }

        foreach(RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null)
                return;

            foreach(RoomNodeSO roomNodeSO in roomNodeGraph.roomNodeList)
            {
                if (roomNodeSO == null)
                    continue;

                if (roomNodeSO.roomNodeType.isEntrance || roomNodeSO.roomNodeType.isCorridorEW || roomNodeSO.roomNodeType.isCorridorNS
                    || roomNodeSO.roomNodeType.isCorridor || roomNodeSO.roomNodeType.isNone)
                    continue;

                bool isRoomNodeTypeFound = false;

                foreach(RoomTemplateSO roomTemplateSO in roomTemplateList)
                {
                    if (roomTemplateSO == null)
                        continue;

                    if (roomTemplateSO.roomNodeType == roomNodeSO.roomNodeType)
                    {
                        isRoomNodeTypeFound = true;
                        break;
                    }
                }

                if (!isRoomNodeTypeFound)
                    Debug.Log("In " + this.name.ToString() + " : No room template " + roomNodeSO.roomNodeType.ToString() + "found for "
                        + roomNodeGraph.name.ToString());
            }
        }
    }


#endif
    #endregion Validation

}
