using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]//需要和对应组件一起使用
public class InstantiatedRoom : MonoBehaviour
{
    [HideInInspector] public Room room;//对应房间
    [HideInInspector] public Grid grid;//id
    [HideInInspector] public Tilemap groundTilemap;//对应的六层tilemap蹭
    [HideInInspector] public Tilemap decoration1Tilemap;
    [HideInInspector] public Tilemap decoration2Tilemap;
    [HideInInspector] public Tilemap frontTilemap;
    [HideInInspector] public Tilemap collisionTilemap;
    [HideInInspector] public Tilemap minimapTilemap;
    [HideInInspector] public Bounds roomColliderBounds;////碰撞边界

    private BoxCollider2D boxCollider2D;//2D碰撞箱组件,用于获取碰撞边界

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        roomColliderBounds = boxCollider2D.bounds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Settings.playerTag && room != GameManager.Instance.GetCurrentRoom())
        {
            this.room.isPreviouslyVisited = true;

            StaticEventHandler.CallRoomChangedEvent(room);
        }
    }

    //实例化
    public void Initialise(GameObject roomGameObject)
    {
        //填充Tilemap的成员变量
        PopulateTilemapMemberVariables(roomGameObject);

        //关闭未被使用的门口
        BlockOffUnusedDoorways();

        //实例化门口区域,用于显示房间和限制战斗区域
        AddDoorsToRooms();

        //关闭碰撞地图快的渲染器
        DisableCollisionTilemapRenderer();

        //设置暗材质
        SetDimmedMaterial();
    }

    public void SetDimmedMaterial()
    {
        groundTilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.dimmedMaterial;
        decoration1Tilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.dimmedMaterial;
        decoration2Tilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.dimmedMaterial;
        frontTilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.dimmedMaterial;
        minimapTilemap.GetComponent<TilemapRenderer>().material = GameResources.Instance.dimmedMaterial;
    }

    private void PopulateTilemapMemberVariables(GameObject roomGameObject)
    {
        grid = roomGameObject.GetComponentInChildren<Grid>();//获取房间孩子中的grid组件

        Tilemap[] tilemaps = roomGameObject.GetComponentsInChildren<Tilemap>();//获取grid孩子中的几个tilemap组件

        foreach(Tilemap tilemap in tilemaps)
        {
            if (tilemap.gameObject.tag == "groundTilemap")
            {
                groundTilemap = tilemap;
            }
            else if (tilemap.gameObject.tag == "decoration1Tilemap")
            {
                decoration1Tilemap = tilemap;
            }
            else if (tilemap.gameObject.tag == "decoration2Tilemap")
            {
                decoration2Tilemap = tilemap;
            }
            else if (tilemap.gameObject.tag == "frontTilemap")
            {
                frontTilemap = tilemap;
            }
            else if (tilemap.gameObject.tag == "collisionTilemap")
            {
                collisionTilemap = tilemap;
            }
            else if (tilemap.gameObject.tag == "minimap Tilemap")
            {
                minimapTilemap = tilemap;
            }
        }
    }

    //封锁所有未连接的门口
    private void BlockOffUnusedDoorways()
    {
        foreach (Doorway doorway in room.doorWayList)
        {
            if (doorway.isConnected)
                continue;

            if (groundTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(groundTilemap, doorway);
            }

            if (decoration1Tilemap != null)
            {
                BlockDoorwayOnTilemapLayer(decoration1Tilemap, doorway);
            }

            if (decoration2Tilemap != null)
            {
                //不复制墙上的装饰物
                //BlockDoorwayOnTilemapLayer(decoration2Tilemap, doorway);
            }

            if (frontTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(frontTilemap, doorway);
            }

            if (collisionTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(collisionTilemap, doorway);
            }

            if (minimapTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(minimapTilemap, doorway);
            }
        }
    }

    private void BlockDoorwayOnTilemapLayer(Tilemap tilemap, Doorway doorway)
    {
        switch (doorway.orientation)
        {
            case Orientation.north:
            case Orientation.south:
                BlockDoorwayHorizontally(tilemap, doorway);
                break;

            case Orientation.east:
            case Orientation.west:
                BlockDoorwayVertically(tilemap, doorway);
                break;

            case Orientation.none:
                break;
        }
    }

    private void BlockDoorwayHorizontally(Tilemap tilemap, Doorway doorway)
    {
        //开始复制的位置,是左上角区域,即要被覆盖的区域左上角往左一格
        Vector2Int startPosition = doorway.doorwayStartCopyPosition;

        //从上到下一排排往右复制
        for (int xpos = 0; xpos < doorway.doorwayCopyTileWidth; xpos++)
        {
            //不复制墙下面的碰撞块
            if (tilemap.gameObject.tag == "collisionTilemap" && xpos == 0) xpos = 1;
            for (int ypos = 0; ypos < doorway.doorwayCopyTileHeight; ypos++)
            {
                //tile的方向
                Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPosition.x + xpos, startPosition.y - ypos, 0));

                //把这一格复制到下一格
                tilemap.SetTile(new Vector3Int(startPosition.x + 1 + xpos, startPosition.y - ypos, 0), 
                    tilemap.GetTile(new Vector3Int(startPosition.x + xpos, startPosition.y - ypos, 0)));

                //把下一格方向和当前格同步,完全复制过去
                tilemap.SetTransformMatrix(new Vector3Int(startPosition.x + 1 + xpos, startPosition.y - ypos, 0), transformMatrix);
            }
        }
    }

    private void BlockDoorwayVertically(Tilemap tilemap, Doorway doorway)
    {
        //开始复制的位置,是左上角区域,即要被覆盖的区域左上角往上一格
        Vector2Int startPosition = doorway.doorwayStartCopyPosition;

        //从右到左一排排往下复制
        for (int ypos = 0; ypos < doorway.doorwayCopyTileHeight; ypos++)
        {
            if (tilemap.gameObject.tag == "collisionTilemap" && ypos == 0) ypos = 1;
            for (int xpos = 0; xpos < doorway.doorwayCopyTileWidth; xpos++)
            {
                //tile的方向
                Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPosition.x + xpos, startPosition.y - ypos, 0));

                //把这一格复制到下一格
                tilemap.SetTile(new Vector3Int(startPosition.x + xpos, startPosition.y - 1 - ypos, 0),
                    tilemap.GetTile(new Vector3Int(startPosition.x + xpos, startPosition.y - ypos, 0)));

                //把下一格方向和当前格同步,完全复制过去
                tilemap.SetTransformMatrix(new Vector3Int(startPosition.x + xpos, startPosition.y - 1 - ypos, 0), transformMatrix);
            }
        }
    }
    private void AddDoorsToRooms()
    {
        if (room.roomNodeType.isCorridorEW || room.roomNodeType.isCorridorNS) return;

        foreach (Doorway doorway in room.doorWayList)
        {
            if (doorway.doorPrefab != null && doorway.isConnected)
            {
                float tileDistance = Settings.tileSizePixels / Settings.pixelsPerUnit;

                GameObject door = null;

                if (doorway.orientation == Orientation.north)
                {
                    door = Instantiate(doorway.doorPrefab, gameObject.transform);
                    //localPosition,相对主物体的位置
                    door.transform.localPosition = new Vector3(doorway.position.x + 0.5f * tileDistance, doorway.position.y + tileDistance, 0f);
                }
                else if (doorway.orientation == Orientation.south)
                {
                    door = Instantiate(doorway.doorPrefab, gameObject.transform);
                    door.transform.localPosition = new Vector3(doorway.position.x + 0.5f * tileDistance, doorway.position.y, 0f);
                }
                else if (doorway.orientation == Orientation.east)
                {
                    door = Instantiate(doorway.doorPrefab, gameObject.transform);
                    door.transform.localPosition = new Vector3(doorway.position.x + tileDistance, doorway.position.y + 0.5f * tileDistance, 0f);
                }
                else if (doorway.orientation == Orientation.west)
                {
                    door = Instantiate(doorway.doorPrefab, gameObject.transform);
                    door.transform.localPosition = new Vector3(doorway.position.x, doorway.position.y + 0.5f * tileDistance, 0f);
                }

                Door doorComponent = door.GetComponent<Door>();

                if (room.roomNodeType.isBossRoom)
                {
                    doorComponent.isBoosRoomDoor = true;

                    doorComponent.LockDoor();
                }
            }
        }
    }

    private void DisableCollisionTilemapRenderer()
    {
        collisionTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
    }
}
