 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string id;
    public string templateID;
    public GameObject prefab;
    public RoomNodeTypeSO roomNodeType;
    public Vector2Int lowerBounds;//与模板的不一样
    public Vector2Int upperBounds;
    public Vector2Int templateLowerBounds;//模板的数据
    public Vector2Int templateUpperBounds;
    public Vector2Int[] spawnPositionArray;
    public List<string> childRoomIDList;
    public string parentRoomID;
    public List<Doorway> doorWayList;
    public bool isPositioned = false;//是否被定位
    public InstantiatedRoom instantiatedRoom;
    public bool isLit = false;//是否已显示
    public bool isClearedOfEnemies = false;
    public bool isPreviouslyVisited = false;//是否曾经被访问过

    public Room()
    {
        childRoomIDList = new List<string>();
        doorWayList = new List<Doorway>();
    }

}
