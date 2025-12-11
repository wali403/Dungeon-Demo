using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "ScriptableObjects/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;

    #region Header ROOM PREFAB

    [Space(10)]
    [Header("ROOM PREFAB")]

    #endregion Header ROOM PREFAB

    #region Tooltip

    [Tooltip("房间的预制体")]

    #endregion Tooltip

    public GameObject prefab;

    [HideInInspector] public GameObject previousPrefab;

    #region Header ROOM CONFIGURATION

    [Space(10)]
    [Header("ROOM CONFIGURATION")]

    #endregion Header ROOM CONFIGURATION

    #region Tooltip

    [Tooltip("房间预制体对应的房间类型")]

    #endregion Tooltip

    public RoomNodeTypeSO roomNodeType;

    #region Tooltip

    [Tooltip("房间的左下角")]

    #endregion Tooltip

    public Vector2Int lowerBounds;

    #region Tooltip

    [Tooltip("房间的右上角")]

    #endregion Tooltip

    public Vector2Int upperBounds;

    #region Tooltip

    [Tooltip("房间的洞口列表,每个房间最多只有四个洞口,东南西北各" +
        "一个,且洞口的大小为三格地砖")]

    #endregion Tooltip

    [SerializeField] public List<Doorway> doorwayList;

    #region Tooltip

    [Tooltip("敌人,宝箱以及玩家小地图传送的有效生成点")]

    #endregion Tooltip
    public Vector2Int[] spawnPositionArray;

    //返回洞口列表
    public List<Doorway> GetDoorwayList()
    {
        return doorwayList;
    }

    #region Validation

#if UNITY_EDITOR

    //验证预制体的字段
    //Unity的回调方法,用于序列化数据变更回调,在编译器中的值被修改时会调用
    private void OnValidate()
    {
        if (guid == "" || previousPrefab != prefab)
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);//保存修改
        }

        //检查洞口
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(doorwayList), doorwayList);

        //检查生成点
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spawnPositionArray), spawnPositionArray);
    }

#endif

    #endregion Validation
}
