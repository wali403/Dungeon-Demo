using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public List<string> parentRoomNodeIDList = new List<string>();
    [HideInInspector] public List<string> childRoomNodeIDList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public Rect rect;
    [HideInInspector] public bool isLeftClickDragging = false;
    [HideInInspector] public bool isSelected = false;

    public void Initialise(Rect rect, RoomNodeGraphSO roomNodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        this.id = Guid.NewGuid().ToString();
        this.name = "RoomNode";
        this.roomNodeGraph = roomNodeGraph;
        this.roomNodeType = roomNodeType;

        //加载房间类型
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        GUILayout.BeginArea(rect, nodeStyle);

        //配合EndChangeCheck使用,令其返回bool值表示是否编译器发生了改变
        EditorGUI.BeginChangeCheck();

        //如果是出口或者有父节点,就设置成标签,否则设置成可选选项
        if (parentRoomNodeIDList.Count > 0 || roomNodeType.isEntrance)
        {
            EditorGUILayout.LabelField(roomNodeType.roomNodeTypeName);
        }
        else
        {
            int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);

            //返回选择的下标值
            //空值或null会表示成横杠隔开
            int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());

            roomNodeType = roomNodeTypeList.list[selection];

            RoomNodeTypeSO selectedRoomNodeType = roomNodeTypeList.list[selected];
            RoomNodeTypeSO selectionRoomNodeType = roomNodeTypeList.list[selection];

            if (selectedRoomNodeType.isCorridor && !selectionRoomNodeType.isCorridor || !selectedRoomNodeType.isCorridor && selectionRoomNodeType.isCorridor
                || !selectedRoomNodeType.isBossRoom && selectionRoomNodeType.isBossRoom)
            {
                if (childRoomNodeIDList.Count > 0)
                {
                    for (int i = childRoomNodeIDList.Count - 1; i >= 0; i--)
                    {
                        string childRoomNodeID = childRoomNodeIDList[i];
                        RoomNodeSO childRoomNode = roomNodeGraph.GetRoomNode(childRoomNodeID);

                        if (childRoomNode != null)
                        {
                            RemoveChildRoomNodeIDFromRoomNode(childRoomNodeID);

                            childRoomNode.RemoveParentRoomNodeIDFromRoomNode(id);
                        }
                    }
                }
            }
        }

        //如果改动了,保存修改
        //SetDirty可以保存编译器中发生的改变
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(this);

        GUILayout.EndArea();
    }

    public string[] GetRoomNodeTypesToDisplay()
    {
        string[] roomArray = new string[roomNodeTypeList.list.Count];

        for (int i = 0; i < roomNodeTypeList.list.Count; i++)
        {
            //如果是需要显示在编译器中的,就把对应名字赋值给字符串数组
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }

        return roomArray;
    }

    public void ProcessEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;

            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;

            default:
                break;
        }
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickDownEvent();
        }
        else if (currentEvent.button == 1)
        {
            ProcessRightClickDownEvent(currentEvent);
        }
    }

    private void ProcessLeftClickDownEvent()
    {
        //让编译器在资产界面选中这个房间
        Selection.activeObject = this;

        isSelected = !isSelected;
    }

    private void ProcessRightClickDownEvent(Event currentEvent)
    {
        roomNodeGraph.SetNodeToDrawConnectionLineFrom(this, currentEvent.mousePosition);
    }

    private void ProcessMouseUpEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickUpEvent();
        }
    }

    private void ProcessLeftClickUpEvent()
    {
        if (isLeftClickDragging)
        {
            isLeftClickDragging = false;
        }
    }

    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickDragEvent(currentEvent);
        }
    }

    private void ProcessLeftClickDragEvent(Event currentEvent)
    {
        isLeftClickDragging = true;

        //delta 捕捉鼠标相对上个事件移动了多少
        DragNode(currentEvent.delta);
        GUI.changed = true;
    }

    public void DragNode(Vector2 delta)
    {
        rect.position += delta;
        EditorUtility.SetDirty(this);
    }

    public bool AddChildRoomNodeIDToRoomNode(string childID)
    {
        if (IsChildRoomValid(childID))
        {
            childRoomNodeIDList.Add(childID);
            return true;
        }

        return false;
    }

    public bool IsChildRoomValid(string childID)
    {
        bool isConnectedBossNodeAlready = false;
        foreach (RoomNodeSO roomNode in roomNodeGraph.roomNodeList)
        {
            if (roomNode.roomNodeType.isBossRoom && roomNode.parentRoomNodeIDList.Count > 0)
                isConnectedBossNodeAlready = true;
        }

        //如果图中已经有了一个被连接的boss房,就不再连接boss房
        if (roomNodeGraph.GetRoomNode(childID).roomNodeType.isBossRoom && isConnectedBossNodeAlready)
            return false;

        //如果子房间是isNone类型,则不连接
        if (roomNodeGraph.GetRoomNode(childID).roomNodeType.isNone)
            return false;

        //如果子房间已经存在了,不重复连接
        if (childRoomNodeIDList.Contains(childID))
            return false;

        //如果子房间是自己,不连接
        if (id == childID)
            return false;

        //如果子房间已经是自己的父房间了,不连接
        if (parentRoomNodeIDList.Contains(childID))
            return false;

        //每个子房间只能有一个父房间
        if (roomNodeGraph.GetRoomNode(childID).parentRoomNodeIDList.Count > 0)
            return false;

        //不允许两个走廊相连
        if (roomNodeGraph.GetRoomNode(childID).roomNodeType.isCorridor && roomNodeType.isCorridor)
            return false;

        //不允许两个房间相连,它们之间必须经过走廊
        if (!roomNodeGraph.GetRoomNode(childID).roomNodeType.isCorridor && !roomNodeType.isCorridor)
            return false;

        //如果一个房间的子走廊达到了设置的最大走廊数,不再增加子走廊
        if (roomNodeGraph.GetRoomNode(childID).roomNodeType.isCorridor && childRoomNodeIDList.Count >= Settings.maxChildCorridors)
            return false;

        //入口是图中最高级别的父房间,不能作为子房间连接
        if (roomNodeGraph.GetRoomNode(childID).roomNodeType.isEntrance)
            return false;

        //一个走廊只能连接一个子房间
        if (!roomNodeGraph.GetRoomNode(childID).roomNodeType.isCorridor && childRoomNodeIDList.Count > 0)
            return false;

        return true;
    }

    public bool AddParentRoomNodeIDToRoomNode(string parentID)
    {
        parentRoomNodeIDList.Add(parentID);
        return true;
    }

    public bool RemoveChildRoomNodeIDFromRoomNode(string childID)
    {
        if (childRoomNodeIDList.Contains(childID))
        {
            childRoomNodeIDList.Remove(childID);
            return true;
        }

        return false;
    }

    public bool RemoveParentRoomNodeIDFromRoomNode(string parentID)
    {
        if (parentRoomNodeIDList.Contains(parentID))
        {
            parentRoomNodeIDList.Remove(parentID);
            return true;
        }

        return false;
    }

#endif

    #endregion Editor Code
}
