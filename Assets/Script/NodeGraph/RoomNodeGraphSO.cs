using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "ScriptableObjects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector] public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>();
    [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>();

    private void Awake()
    {
        LoadRoomNodeDictionary();
    }

    private void LoadRoomNodeDictionary()
    {
        roomNodeDictionary.Clear();

        foreach (RoomNodeSO node in roomNodeList)
        {
            roomNodeDictionary[node.id] = node;
        }
    }

    //传入房间类型返回对应房间
    //用于寻找入口
    public RoomNodeSO GetRoomNode(RoomNodeTypeSO roomNodeType)
    {
        foreach(RoomNodeSO node in roomNodeList)
        {
            if(node.roomNodeType == roomNodeType)
            {
                return node;
            }
        }
        return null;
    }

    public RoomNodeSO GetRoomNode(string roomNodeID)
    {
        if (roomNodeDictionary.TryGetValue(roomNodeID, out RoomNodeSO roomNode))
        {
            return roomNode;
        }
        return null;
    }

    //IEnumerable<T>或者IEnumerator<T> 配合 yeild return
    //当从这个方法生成的集合中拿单位时,每次拿取一个才会执行一次方法的返回
    //而不是一次性把集合所有单位都取好
    public IEnumerable<RoomNodeSO> GetChildRoomNodes(RoomNodeSO parrentRoomNode)
    {
        foreach(string childNodeID in parrentRoomNode.childRoomNodeIDList)
        {
            yield return GetRoomNode(childNodeID);
        }
    }

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null;
    [HideInInspector] public Vector2 linePosition;

    //编译器中发生改变时,重新加载字典
    public void OnValidate()
    {
        LoadRoomNodeDictionary();
    }

    public void SetNodeToDrawConnectionLineFrom(RoomNodeSO node, Vector2 position)
    {
        roomNodeToDrawLineFrom = node;
        linePosition = position;
    }




#endif

#endregion Editor Code
}
